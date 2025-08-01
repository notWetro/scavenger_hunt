from fastapi import FastAPI, Depends, Body, HTTPException, status
from sqlalchemy.ext.asyncio import (
    AsyncSession,
    async_sessionmaker,
    create_async_engine,
)
from sqlalchemy.orm import declarative_base

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

from fastapi.middleware.cors import CORSMiddleware

from typing import List, Optional

from pydantic import BaseModel

from dotenv import load_dotenv
import os

load_dotenv()


# === PostgreSQL DATABASE CONFIG ===
DATABASE_URL = os.getenv("DATABASE_URL")


# === SQLAlchemy Setup ===
async_engine = create_async_engine(DATABASE_URL, echo=False, future=True)
AsyncSessionLocal = async_sessionmaker(
    async_engine, expire_on_commit=False, autoflush=False
)
Base = declarative_base()

# === FastAPI App ===
app = FastAPI()

app.add_middleware(
  CORSMiddleware,
  allow_origins=["http://localhost:3000", "http://werwoelfe.fun:3000"],
  allow_credentials=True,
  allow_methods=["*"],
  allow_headers=["*"],
)
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


# Dependency to get async DB session
async def get_db():
    async with AsyncSessionLocal() as session:
        yield session


async def get_user_db(session: AsyncSession = Depends(get_db)):
    yield SQLAlchemyUserDatabase(session, User)



SECRET = os.getenv("SECRET_KEY")

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


# Create empty hunt
@app.post("/create-hunt")
async def create_hunt(
    name: str = Body(..., embed=True),
    current_user: User = Depends(fastapi_users.current_user(active=True)),
    db: AsyncSession = Depends(get_db),
):
    new_hunt = Hunt(
        name=name,
        description="",
        place_to_play="",
        start_point="",
        created_by=current_user.id,
        is_active=False,
        private=False,
        created_at=datetime.utcnow()
    )
    db.add(new_hunt)
    await db.commit()
    await db.refresh(new_hunt)

    return {
        "hunt": {"id": new_hunt.id}
    }


class HuntUpdate(BaseModel):
    name:          Optional[str] = None
    description:   Optional[str] = None
    place_to_play: Optional[str] = None
    start_point:   Optional[str] = None
    is_active:     Optional[bool]  = None
    private:       Optional[bool]  = None

class HuntRead(BaseModel):
    id:            int
    name:          str
    description:   Optional[str]
    place_to_play: Optional[str]
    start_point:   Optional[str]
    is_active:     bool
    private:       bool
    created_by:    int
    created_at:    datetime
    creator_username: Optional[str] = None  

    class Config:
        orm_mode = True


# Update hunt
@app.put("/hunts/{hunt_id}", response_model=HuntRead)
async def update_hunt(
    hunt_id: int,
    payload: HuntUpdate,
    db: AsyncSession = Depends(get_db),
):
    result = await db.execute(select(Hunt).filter(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt:
        raise HTTPException(404, "Hunt not found")

    for field, value in payload.dict(exclude_unset=True).items():
        setattr(hunt, field, value)

    await db.commit()
    await db.refresh(hunt)

    return hunt


# Get hunt
@app.get("/hunts/{hunt_id}")
async def get_specific_hunt(hunt_id: int, db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Hunt).filter(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt:
        return {"error": "Hunt not found"}
    
    creator_res = await db.execute(
        select(User.username).where(User.id == hunt.created_by)
    )
    creator_username = creator_res.scalars().first() or "Unknown"

    return {
        "id": hunt.id,
        "name": hunt.name,
        "description": hunt.description,
        "place_to_play": hunt.place_to_play,
        "start_point": hunt.start_point,
        "is_active": hunt.is_active,
        "created_by": hunt.created_by,
        "created_at": hunt.created_at,
        "private": hunt.private,
        "creator_username": creator_username
    }


class ClueRead(BaseModel):
    id: int
    title: Optional[str]
    description: Optional[str]
    hint: Optional[str]
    correct_answer: Optional[str]
    clue_order: Optional[int]
    image_url: Optional[str]
    audio_url: Optional[str]
    video_url: Optional[str]
    question_type: Optional[str]
    answer_type: Optional[str]
    choices: Optional[str]
    expected_gps: Optional[str]
    gps_radius: Optional[float]

    class Config:
        orm_mode = True

# Get specific clue
@app.get("/hunts/{hunt_id}/clues/{clue_id}", response_model=ClueRead)
async def get_clue(
    hunt_id: int,
    clue_id: int,
    current_user: User = Depends(fastapi_users.current_user()),
    db: AsyncSession = Depends(get_db),
):
    # verify hunt ownership
    user_result = await db.execute(select(Hunt).where(Hunt.id == hunt_id))
    hunt = user_result.scalars().first()
    if not hunt:
        raise HTTPException(404, "Hunt not found")
    if hunt.created_by != current_user.id:
        raise HTTPException(403, "You are not allowed to access this hunt")
    
    result = await db.execute(
        select(Clue).where(Clue.id == clue_id, Clue.hunt_id == hunt_id)
    )
    clue = result.scalars().first()
    if not clue:
        raise HTTPException(404, "Clue not found")
    return clue

# response schema for a clue
class ClueRead(BaseModel):
    id:             int
    description:    str | None  
    correct_answer: str | None
    clue_order:    int | None
    hint:           str | None
    question_type:   str | None
    answer_type:     str | None
    choices:        str | None
    image_url:      str | None
    audio_url:      str | None
    video_url:      str | None
    expected_gps:   str | None
    gps_radius:     float | None

    class Config:
        orm_mode = True

# List clues for a specific hunt
@app.get("/hunts/{hunt_id}/clues", response_model=List[ClueRead])
async def list_clues_for_hunt(
    hunt_id: int,
    db: AsyncSession = Depends(get_db),
):
    result = await db.execute(
        select(Clue)
        .where(Clue.hunt_id == hunt_id)
        .order_by(Clue.clue_order)
    )
    return result.scalars().all()


class ClueCreateResponse(BaseModel):
    id: int

class ClueCreate(BaseModel):
    clue_order: Optional[int] = None

# Create empty clue
@app.post("/hunts/{hunt_id}/clues", response_model=ClueCreateResponse, status_code=201)
async def create_empty_clue(
    hunt_id: int,
    payload: ClueCreate,
    db: AsyncSession = Depends(get_db),
    current_user: User = Depends(fastapi_users.current_user()),
):
    # verify current_user.id == hunt.created_by
    result = await db.execute(select(Hunt).filter(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt:
        return {"error": "Hunt not found"}
    if hunt.created_by != current_user.id:
        return {"error": "You are not allowed to create clues for this hunt"}
    
    if payload.clue_order is None:
        # count existing clues
        count = (
            await db.execute(
                select(func.count()).select_from(Clue).where(Clue.hunt_id == hunt_id)
            )
        ).scalar_one()
        order = count + 1
    else:
        order = payload.clue_order
    
    new_clue = Clue(
        hunt_id=hunt_id,
        title="",
        description="",
        clue_order=order,
        created_at=datetime.utcnow(),
    )
    db.add(new_clue)
    await db.commit()
    await db.refresh(new_clue)
    return {"id": new_clue.id}


# Delete clue
@app.delete("/hunts/{hunt_id}/clues/{clue_id}", status_code=status.HTTP_204_NO_CONTENT)
async def delete_clue(
    hunt_id: int,
    clue_id: int,
    current_user: User = Depends(fastapi_users.current_user()),
    db: AsyncSession = Depends(get_db),
):
    # ensure the hunt exists and belongs to the user
    result = await db.execute(select(Hunt).where(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt or hunt.created_by != current_user.id:
        raise HTTPException(status.HTTP_404_NOT_FOUND, "Hunt not found")

    # fetch the clue
    result = await db.execute(
        select(Clue).where(Clue.id == clue_id, Clue.hunt_id == hunt_id)
    )
    clue = result.scalars().first()
    if not clue:
        raise HTTPException(status.HTTP_404_NOT_FOUND, "Clue not found")

    # delete it
    await db.delete(clue)
    await db.commit()
    # 204 → no body

class ClueUpdate(BaseModel):
    title: Optional[str] = None
    description: Optional[str] = None
    hint: Optional[str] = None
    correct_answer: Optional[str] = None
    clue_order: Optional[int] = None
    image_url: Optional[str] = None
    audio_url: Optional[str] = None
    video_url: Optional[str] = None
    question_type: Optional[str] = None
    answer_type: Optional[str] = None
    choices: Optional[str] = None
    expected_gps: Optional[str] = None
    gps_radius: Optional[float] = None

# Update clue
@app.patch("/hunts/{hunt_id}/clues/{clue_id}", response_model=ClueRead)
async def update_clue(
    hunt_id: int,
    clue_id: int,
    payload: ClueUpdate,
    current_user: User = Depends(fastapi_users.current_user()),
    db: AsyncSession = Depends(get_db),
):
    # check hunt exists & belongs to user
    user_result = await db.execute(select(Hunt).where(Hunt.id == hunt_id))
    hunt = user_result.scalars().first()
    if not hunt or hunt.created_by != current_user.id:
        raise HTTPException(status.HTTP_404_NOT_FOUND, "Hunt not found")
    if hunt.created_by != current_user.id:
        raise HTTPException(status.HTTP_403_FORBIDDEN, "Not allowed to modify this hunt")
    

    # check clue exists in hunt
    result = await db.execute(
        select(Clue).where(Clue.id == clue_id, Clue.hunt_id == hunt_id)
    )
    clue = result.scalars().first()
    if not clue:
        raise HTTPException(status.HTTP_404_NOT_FOUND, "Clue not found")

    for field, value in payload.dict(exclude_unset=True).items():
        setattr(clue, field, value)

    await db.commit()
    await db.refresh(clue)
    return clue


class JoinHuntResponse(BaseModel):
    message: str
    hunt_id: int
    place_to_play: str
    start_point: str
    creator_username: str

    class Config:
        orm_mode = True

# Join hunt
@app.post("/hunts/{hunt_id}/join",response_model=JoinHuntResponse,status_code=status.HTTP_201_CREATED)
async def join_hunt(
    hunt_id: int,
    current_user: Optional[User] = Depends(
        fastapi_users.current_user(optional=True)
    ),
    db: AsyncSession = Depends(get_db),
):
    result = await db.execute(select(Hunt).where(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt:
        raise HTTPException(404, "Hunt not found")

    if current_user:
        
        progress = UserHuntProgress(
            hunt_id=hunt_id,
            user_id=current_user.id,
            current_clue_id=None,
            finished_at=None,
        )
        db.add(progress)
        await db.commit()
        await db.refresh(progress)

    creator_res = await db.execute(
        select(User.username).where(User.id == hunt.created_by)
    )
    creator_username = creator_res.scalars().first() or "Unknown"

    return JoinHuntResponse(
        message="Joined hunt",
        hunt_id=hunt.id,
        place_to_play=hunt.place_to_play,
        start_point=hunt.start_point,
        creator_username=creator_username,
    )

# Remove user from hunt
@app.delete("/hunts/{hunt_id}/leave",response_model=BaseModel, status_code=status.HTTP_200_OK)
async def leave_hunt(
    hunt_id: int,
    current_user: Optional[User] = Depends(
        fastapi_users.current_user(optional=True)
    ),
    db: AsyncSession = Depends(get_db),
):
    if not current_user:
        # you could also return 204 No Content to be idempotent for anonymous
        raise HTTPException(status.HTTP_401_UNAUTHORIZED, "Not authenticated")

    # find their progress record
    result = await db.execute(
        select(UserHuntProgress).where(
            UserHuntProgress.hunt_id == hunt_id,
            UserHuntProgress.user_id == current_user.id,
        )
    )
    progress = result.scalars().first()
    if not progress:
        raise HTTPException(
            status.HTTP_404_NOT_FOUND,
            "You are not part of this hunt",
        )

    await db.delete(progress)
    await db.commit()

    return {"message": "Left the hunt successfully"}

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


class CurrentClueResponse(BaseModel):
    current_clue_id: int | None

    class Config:
        orm_mode = True

# Get current clue for user in a hunt
@app.get("/hunts/{hunt_id}/current-clue", response_model=CurrentClueResponse, summary="Get the current clue for this user in a hunt")
async def get_current_clue(
    hunt_id: int,
    current_user: Optional[User] = Depends(
        fastapi_users.current_user(optional=True)
    ),
    db: AsyncSession = Depends(get_db),
):  
    if current_user:
        result = await db.execute(
            select(UserHuntProgress)
            .where(
                UserHuntProgress.hunt_id == hunt_id,
                UserHuntProgress.user_id == current_user.id,
            )
        )
    
        progress = result.scalars().first()
        if not progress:
            raise HTTPException(
                status.HTTP_404_NOT_FOUND, "User has not joined this hunt"
            )

        return {"current_clue_id": progress.current_clue_id}
    else:
        return {"current_clue_id": 0}
    
class ProgressPayload(BaseModel):
    clue_id: int

# Save progress when user solves a clue
@app.post("/hunts/{hunt_id}/progress",status_code=status.HTTP_204_NO_CONTENT,summary="Record that the user reached/solved a clue")
async def save_progress(
    hunt_id: int,
    payload: ProgressPayload,
    current_user: Optional[User] = Depends(
        fastapi_users.current_user(optional=True)
    ),
    db: AsyncSession = Depends(get_db),
):
    if not current_user:
        return 

    result = await db.execute(select(Hunt).where(Hunt.id == hunt_id))
    hunt = result.scalars().first()
    if not hunt:
        raise HTTPException(status.HTTP_404_NOT_FOUND, "Hunt not found")

    res = await db.execute(
        select(UserClueProgress).where(
            UserClueProgress.user_id == current_user.id,
            UserClueProgress.clue_id == payload.clue_id,
        )
    )
    ucp = res.scalars().first()
    if not ucp:
        ucp = UserClueProgress(
            user_id=current_user.id,
            clue_id=payload.clue_id,
            is_solved=True,
            solved_at=datetime.utcnow(),
        )
        db.add(ucp)
    else:
        ucp.is_solved = True
        ucp.solved_at = datetime.utcnow()

    res = await db.execute(
        select(UserHuntProgress).where(
            UserHuntProgress.user_id == current_user.id,
            UserHuntProgress.hunt_id == hunt_id,
        )
    )
    uhp = res.scalars().first()
    if uhp:
        r2 = await db.execute(
            select(Clue).where(Clue.hunt_id == hunt_id).order_by(Clue.clue_order)
        )
        clues = r2.scalars().all()
        idx = next((i for i,c in enumerate(clues) if c.id == payload.clue_id), None)
        
        if idx is not None and idx + 1 < len(clues):
            uhp.current_clue_id = clues[idx + 1].id
        else:
            uhp.current_clue_id = None  

    
    await db.commit()
    

# List hunts the current user has joined
@app.get(
    "/hunts/search/joined",
    response_model=List[HuntRead],
    summary="List hunts the current user has joined",
)
async def list_joined_hunts(
    current_user: User = Depends(fastapi_users.current_user(active=True)),
    db: AsyncSession = Depends(get_db),
):
    result = await db.execute(
        select(Hunt)
        .join(UserHuntProgress, UserHuntProgress.hunt_id == Hunt.id)
        .where(UserHuntProgress.user_id == current_user.id)
        .order_by(Hunt.created_at.desc())
    )
    hunts = result.scalars().all()
    return hunts


# List hunts the current user owns
@app.get(
    "/hunts/search/own",
    response_model=List[HuntRead],
    summary="List hunts the current user owns",
)
async def list_own_hunts(
    current_user: User = Depends(fastapi_users.current_user(active=True)),
    db: AsyncSession = Depends(get_db),
):
    result = await db.execute(
        select(Hunt)
        .where(Hunt.created_by == current_user.id)
        .order_by(Hunt.created_at.desc())
    )
    hunts = result.scalars().all()
    return hunts


# List all public hunts (not private)
@app.get(
    "/hunts/search/public",
    response_model=List[HuntRead],
    summary="List all public hunts",
)
async def list_public_hunts(
    db: AsyncSession = Depends(get_db),
):
    result = await db.execute(
        select(Hunt)
        .where(Hunt.private == False)
        .order_by(Hunt.created_at.desc())
    )
    hunts = result.scalars().all()
    return hunts