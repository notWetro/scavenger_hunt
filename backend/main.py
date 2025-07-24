from fastapi import FastAPI, Depends

from sqlalchemy.ext.asyncio import (
    AsyncSession,
    async_sessionmaker,
    create_async_engine,
)
from sqlalchemy.orm import declarative_base, Session

from sqlalchemy import (
    Column,
    Integer,
    String,
    Boolean,
    Float,
    Text,
    ForeignKey,
    DateTime,
    select
)


from datetime import datetime
from fastapi_users.manager import BaseUserManager
from fastapi_users import FastAPIUsers
from fastapi_users.authentication import AuthenticationBackend, BearerTransport, JWTStrategy
from fastapi_users_db_sqlalchemy import (
     SQLAlchemyUserDatabase,
     SQLAlchemyBaseUserTable,
 )
from schemas import UserRead, UserCreate, UserUpdate



# === PostgreSQL DATABASE CONFIG ===
DATABASE_URL = "postgresql+asyncpg://postgres:1967@db:5432/scavengerhunt"


# === SQLAlchemy Setup ===
async_engine = create_async_engine(DATABASE_URL, echo=False, future=True)
AsyncSessionLocal = async_sessionmaker(
    async_engine, expire_on_commit=False, autoflush=False
)
Base = declarative_base()

# === FastAPI App ===
app = FastAPI()


# === Dependency to get DB session ===
def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()


# === MODELS ===

class User(SQLAlchemyBaseUserTable[int], Base):
    __tablename__ = "users"

    # --- primary key (required when you use SQLAlchemyBaseUserTable[int]) ---
    id: int = Column(Integer, primary_key=True, autoincrement=True)

    # --- your extra fields --------------------------------------------------
    username   = Column(String, nullable=False)
    language   = Column(String, default="en")
    dark_mode  = Column(Boolean, default=False)
    created_at = Column(DateTime, default=datetime.utcnow)




async def get_async_db():
    async with AsyncSessionLocal() as session:
        yield session

async def get_user_db(session: AsyncSession = Depends(get_async_db)):
    yield SQLAlchemyUserDatabase(session, User)


    
SECRET = "SUPER_SECRET"  # !!!!!!!!!!!!!!!!!!!!!!!!move to env in prod

class UserManager(BaseUserManager[User, int]):
    user_db_model = User
    reset_password_token_secret = SECRET
    verification_token_secret = SECRET

    def parse_id(self, user_id: str | int) -> int:  # or UUID, etc.
        
        return int(user_id)

async def get_user_manager(user_db=Depends(get_user_db)):
    yield UserManager(user_db)

bearer_transport = BearerTransport(tokenUrl="auth/jwt/login")
def get_jwt_strategy() -> JWTStrategy:
    return JWTStrategy(secret=SECRET, lifetime_seconds=3600)

auth_backend = AuthenticationBackend(
    name="jwt",
    transport=bearer_transport,
    get_strategy=get_jwt_strategy,
)

fastapi_users = FastAPIUsers(
    get_user_manager,   # your new manager factory
    [auth_backend],     # authentication backend(s)
)


# Auth (login + refresh)
app.include_router(
    fastapi_users.get_auth_router(auth_backend), 
    prefix="/auth/jwt", tags=["auth"]
)

# Register + verify
app.include_router(
    fastapi_users.get_register_router(UserRead, UserCreate),      
    prefix="/auth", tags=["auth"]
)
app.include_router(
    fastapi_users.get_verify_router(UserRead),        
    prefix="/auth", tags=["auth"]
)


# Password reset
app.include_router(
    fastapi_users.get_reset_password_router(),  
    prefix="/auth", tags=["auth"]
)


# User management (read, update, delete)
app.include_router(
    fastapi_users.get_users_router(UserRead, UserUpdate),         
    prefix="/users", tags=["users"]
)



class Hunt(Base):
    __tablename__ = "hunt"

    id = Column(Integer, primary_key=True, index=True)
    name = Column(String, nullable=False)
    description = Column(Text)
    place_to_play = Column(Text)
    start_point = Column(Text)
    created_by = Column(Integer, ForeignKey("users.id"), nullable=False)
    is_active = Column(Boolean, default=True)
    private = Column(Boolean, default=False)
    created_at = Column(DateTime, default=datetime.utcnow)


class Clue(Base):
    __tablename__ = "clue"

    id = Column(Integer, primary_key=True, index=True)
    hunt_id = Column(Integer, ForeignKey("hunt.id"), nullable=False)
    title = Column(String, nullable=False)
    description = Column(Text)
    hint = Column(Text)
    correct_answer = Column(String)
    clue_order = Column(Integer)

    image_url = Column(String)
    audio_url = Column(String)
    video_url = Column(String)

    created_at = Column(DateTime, default=datetime.utcnow)

    question_type = Column(String)         
    answer_type = Column(String)           
    choices = Column(Text)                 # JSON 
    expected_gps = Column(String)          
    gps_radius = Column(Float)


class UserHuntProgress(Base):
    __tablename__ = "user_hunt_progress"

    id = Column(Integer, primary_key=True, index=True)
    user_id = Column(Integer, ForeignKey("users.id"), nullable=False)
    hunt_id = Column(Integer, ForeignKey("hunt.id"), nullable=False)
    current_clue_id = Column(Integer, ForeignKey("clue.id"))
    finished_at = Column(DateTime)


class UserClueProgress(Base):
    __tablename__ = "user_clue_progress"

    id = Column(Integer, primary_key=True, index=True)
    user_id = Column(Integer, ForeignKey("users.id"), nullable=False)
    clue_id = Column(Integer, ForeignKey("clue.id"), nullable=False)
    is_solved = Column(Boolean, default=False)
    solved_at = Column(DateTime)


# === Table Initialization ===
@app.on_event("startup")
async def on_startup() -> None:
    async with async_engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)





""" @app.get("/users")
def get_users(db: Session = Depends(get_db)):
    return db.query(User).all()


@app.get("/hunts")
def get_hunts(db: Session = Depends(get_db)):
    return db.query(Hunt).all()


@app.get("/clues")
def get_clues(db: Session = Depends(get_db)):
    return db.query(Clue).all()


@app.get("/user-hunt-progress")
def get_user_hunt_progress(db: Session = Depends(get_db)):
    return db.query(UserHuntProgress).all()


@app.get("/user-clue-progress")
def get_user_clue_progress(db: Session = Depends(get_db)):
    return db.query(UserClueProgress).all()
 """
# Dependency to get async DB session
async def get_db():
    async with AsyncSessionLocal() as session:
        yield session

# Create empty hunt
@app.post("/create-hunt")
async def create_hunt(user_id: int, db: AsyncSession = Depends(get_db)):
    new_hunt = Hunt(
        name="Untitled Hunt",
        description="",
        place_to_play="",
        start_point="",
        created_by=user_id,
        is_active=False,
        private=False,
        created_at=datetime.utcnow()
    )
    db.add(new_hunt)
    await db.commit()
    await db.refresh(new_hunt)

    return {
        "id": new_hunt.id,
        "message": "New hunt created",
        "hunt": {
            "id": new_hunt.id,
            "name": new_hunt.name,
            "description": new_hunt.description,
            "is_active": new_hunt.is_active
        }
    }

# Update hunt
@app.put("/update-hunt/{hunt_id}")
async def update_hunt(hunt_id: int, name: str = None, description: str = None, place_to_play: str = None, start_point: str = None, is_active: bool = None, private: bool = None, db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Hunt).filter(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt:
        return {"error": "Hunt not found"}

    if name is not None:
        hunt.name = name
    if description is not None:
        hunt.description = description
    if place_to_play is not None:
        hunt.place_to_play = place_to_play
    if start_point is not None:
        hunt.start_point = start_point
    if is_active is not None:
        hunt.is_active = is_active
    if private is not None:
        hunt.private = private

    await db.commit()
    await db.refresh(hunt)

    return {
        "id": hunt.id,
        "message": "Hunt updated successfully",
        "hunt": {
            "id": hunt.id,
            "name": hunt.name,
            "description": hunt.description,
            "place_to_play": hunt.place_to_play,
            "start_point": hunt.start_point,
            "is_active": hunt.is_active,
            "private": hunt.private
        }
    }

# Get hunt
@app.get("/hunts/{hunt_id}")
async def get_specific_hunt(hunt_id: int, db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Hunt).filter(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt:
        return {"error": "Hunt not found"}

    return {
        "id": hunt.id,
        "name": hunt.name,
        "description": hunt.description,
        "place_to_play": hunt.place_to_play,
        "start_point": hunt.start_point,
        "is_active": hunt.is_active,
        "created_by": hunt.created_by,
        "created_at": hunt.created_at,
        "private": hunt.private
    }

# Get specific clue
@app.get("/hunts/{hunt_id}/clues/{clue_id}")
async def get_specific_clue(hunt_id: int, clue_id: int, db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Clue).filter(Clue.hunt_id == hunt_id, Clue.id == clue_id))
    clue = result.scalars().first()
    if not clue:
        return {"error": "Clue not found for this hunt"}

    return {
        "hunt_id": hunt_id,
        "clue": {
            "id": clue.id,
            "title": clue.title,
            "description": clue.description,
            "hint": clue.hint,
            "correct_answer": clue.correct_answer,
            "clue_order": clue.clue_order,
            "image_url": clue.image_url,
            "audio_url": clue.audio_url,
            "video_url": clue.video_url,
            "question_type": clue.question_type,
            "answer_type": clue.answer_type,
            "choices": clue.choices,
            "expected_gps": clue.expected_gps,
            "gps_radius": clue.gps_radius
        }
    }

# Update clue
@app.put("/hunts/{hunt_id}/clues/{clue_id}")
async def update_clue(hunt_id: int, clue_id: int, title: str = None, description: str = None, hint: str = None, correct_answer: str = None, clue_order: int = None, image_url: str = None, audio_url: str = None, video_url: str = None, question_type: str = None, answer_type: str = None, choices: str = None, expected_gps: str = None, gps_radius: float = None, db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Clue).filter(Clue.hunt_id == hunt_id, Clue.id == clue_id))
    clue = result.scalars().first()
    if not clue:
        return {"error": "Clue not found for this hunt"}

    if title is not None:
        clue.title = title
    if description is not None:
        clue.description = description
    if hint is not None:
        clue.hint = hint
    if correct_answer is not None:
        clue.correct_answer = correct_answer
    if clue_order is not None:
        clue.clue_order = clue_order
    if image_url is not None:
        clue.image_url = image_url
    if audio_url is not None:
        clue.audio_url = audio_url
    if video_url is not None:
        clue.video_url = video_url
    if question_type is not None:
        clue.question_type = question_type
    if answer_type is not None:
        clue.answer_type = answer_type
    if choices is not None:
        clue.choices = choices
    if expected_gps is not None:
        clue.expected_gps = expected_gps
    if gps_radius is not None:
        clue.gps_radius = gps_radius

    await db.commit()
    await db.refresh(clue)

    return {
        "hunt_id": hunt_id,
        "clue": {
            "id": clue.id,
            "title": clue.title,
            "description": clue.description,
            "hint": clue.hint,
            "correct_answer": clue.correct_answer,
            "clue_order": clue.clue_order,
            "image_url": clue.image_url,
            "audio_url": clue.audio_url,
            "video_url": clue.video_url,
            "question_type": clue.question_type,
            "answer_type": clue.answer_type,
            "choices": clue.choices,
            "expected_gps": clue.expected_gps,
            "gps_radius": clue.gps_radius
        },
        "message": "Clue updated successfully"
    }

# Create empty clue
@app.post("/hunts/{hunt_id}/clues")
async def create_empty_clue(hunt_id: int, db: AsyncSession = Depends(get_db)):
    new_clue = Clue(
        hunt_id=hunt_id,
        title="Untitled Clue",
        description="",
        hint="",
        correct_answer="",
        clue_order=0,
        image_url="",
        audio_url="",
        video_url="",
        question_type="",
        answer_type="",
        choices="",
        expected_gps="",
        gps_radius=0.0,
        created_at=datetime.utcnow()
    )
    db.add(new_clue)
    await db.commit()
    await db.refresh(new_clue)

    return {
        "hunt_id": hunt_id,
        "message": "New clue created",
        "clue": {
            "id": new_clue.id,
            "title": new_clue.title,
            "description": new_clue.description,
            "hint": new_clue.hint,
            "correct_answer": new_clue.correct_answer,
            "clue_order": new_clue.clue_order,
            "image_url": new_clue.image_url,
            "audio_url": new_clue.audio_url,
            "video_url": new_clue.video_url,
            "question_type": new_clue.question_type,
            "answer_type": new_clue.answer_type,
            "choices": new_clue.choices,
            "expected_gps": new_clue.expected_gps,
            "gps_radius": new_clue.gps_radius
        }
    }

# Join hunt
@app.post("/join-hunt/{hunt_id}")
async def join_hunt(hunt_id: int, user_id: int, db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Hunt).filter(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt:
        return {"error": "Hunt not found"}

    user_hunt_progress = UserHuntProgress(
        user_id=user_id,
        hunt_id=hunt_id,
        current_clue_id=None,
        finished_at=None
    )
    db.add(user_hunt_progress)
    await db.commit()
    await db.refresh(user_hunt_progress)

    return {
        "message": "User successfully joined the hunt",
        "hunt": {
            "id": hunt.id,
            "name": hunt.name,
            "description": hunt.description,
            "place_to_play": hunt.place_to_play,
            "start_point": hunt.start_point,
            "is_active": hunt.is_active,
            "created_by": hunt.created_by,
            "created_at": hunt.created_at,
            "private": hunt.private,
        }
    }

# Remove user from hunt
@app.delete("/remove-user-from-hunt/{hunt_id}")
async def remove_user_from_hunt(hunt_id: int, user_id: int, db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(UserHuntProgress).filter(
        UserHuntProgress.hunt_id == hunt_id, UserHuntProgress.user_id == user_id
    ))
    user_hunt_progress = result.scalars().first()

    if not user_hunt_progress:
        return {"error": "User is not part of this hunt"}

    await db.delete(user_hunt_progress)
    await db.commit()

    return {"message": "User successfully removed from the hunt"}

# Start hunt with skipping solved clues and return all current clue details
@app.post("/start-hunt/{hunt_id}")
async def start_hunt(hunt_id: int, user_id: int, db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Hunt).filter(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt:
        return {"error": "Hunt not found"}

    result = await db.execute(select(UserHuntProgress).filter(
        UserHuntProgress.hunt_id == hunt_id, UserHuntProgress.user_id == user_id
    ))
    user_hunt_progress = result.scalars().first()

    if not user_hunt_progress:
        return {"error": "User is not part of this hunt"}

    result = await db.execute(select(Clue).filter(Clue.hunt_id == hunt_id).order_by(Clue.clue_order))
    clues = result.scalars().all()
    if not clues:
        return {"error": "No clues available for this hunt"}

    for clue in clues:
        result = await db.execute(select(UserClueProgress).filter(
            UserClueProgress.user_id == user_id,
            UserClueProgress.clue_id == clue.id,
            UserClueProgress.is_solved == True
        ))
        solved = result.scalars().first()
        if not solved:
            user_hunt_progress.current_clue_id = clue.id
            await db.commit()
            await db.refresh(user_hunt_progress)

            return {
                "message": "Hunt started successfully",
                "current_clue": {
                    "id": clue.id,
                    "title": clue.title,
                    "description": clue.description,
                    "hint": clue.hint,
                    "clue_order": clue.clue_order,
                    "question_type": clue.question_type,
                    "answer_type": clue.answer_type,
                    "choices": clue.choices,
                    "image_url": clue.image_url,
                    "audio_url": clue.audio_url,
                    "video_url": clue.video_url,
                    "expected_gps": clue.expected_gps,
                    "gps_radius": clue.gps_radius
                }
            }

    return {"message": "All clues have already been solved"}

# Check if the answer is correct
@app.post("/check-answer/{clue_id}")
async def check_if_answer_true(clue_id: int, user_id: int, answer: str, db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Clue).filter(Clue.id == clue_id))
    clue = result.scalars().first()
    if not clue:
        return {"error": "Clue not found"}

    if clue.correct_answer.lower() == answer.lower():
        result = await db.execute(select(UserClueProgress).filter(
            UserClueProgress.user_id == user_id,
            UserClueProgress.clue_id == clue_id
        ))
        user_clue_progress = result.scalars().first()

        if not user_clue_progress:
            user_clue_progress = UserClueProgress(
                user_id=user_id,
                clue_id=clue_id,
                is_solved=True,
                solved_at=datetime.utcnow()
            )
            db.add(user_clue_progress)
        else:
            user_clue_progress.is_solved = True
            user_clue_progress.solved_at = datetime.utcnow()

        await db.commit()
        await db.refresh(user_clue_progress)

        return {"message": "Correct answer", "is_correct": True}

    return {"message": "Incorrect answer", "is_correct": False}    


