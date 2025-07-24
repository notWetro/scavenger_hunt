from fastapi import FastAPI, Depends
from sqlalchemy import (
    create_engine, Column, Integer, String, Boolean, Float, Text,
    ForeignKey, DateTime
)
from sqlalchemy.orm import sessionmaker, declarative_base, Session
from datetime import datetime

# === PostgreSQL DATABASE CONFIG ===
DATABASE_URL = "postgresql://postgres:1967@db:5432/scavengerhunt"


# === SQLAlchemy Setup ===
engine = create_engine(DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)
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

class User(Base):
    __tablename__ = "user"

    id = Column(Integer, primary_key=True, index=True)
    username = Column(String, nullable=False)
    email = Column(String, unique=True, index=True)
    language = Column(String, default="en")
    dark_mode = Column(Boolean, default=False)
    created_at = Column(DateTime, default=datetime.utcnow)


class Hunt(Base):
    __tablename__ = "hunt"

    id = Column(Integer, primary_key=True, index=True)
    name = Column(String, nullable=False)
    description = Column(Text)
    place_to_play = Column(Text)
    start_point = Column(Text)
    created_by = Column(Integer, ForeignKey("user.id"), nullable=False)
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
    user_id = Column(Integer, ForeignKey("user.id"), nullable=False)
    hunt_id = Column(Integer, ForeignKey("hunt.id"), nullable=False)
    current_clue_id = Column(Integer, ForeignKey("clue.id"))
    finished_at = Column(DateTime)


class UserClueProgress(Base):
    __tablename__ = "user_clue_progress"

    id = Column(Integer, primary_key=True, index=True)
    user_id = Column(Integer, ForeignKey("user.id"), nullable=False)
    clue_id = Column(Integer, ForeignKey("clue.id"), nullable=False)
    is_solved = Column(Boolean, default=False)
    solved_at = Column(DateTime)


# === Table Initialization ===
@app.on_event("startup")
def on_startup():
    Base.metadata.create_all(bind=engine)




@app.get("/users")
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

# Api endpoint to add a new user
@app.post("/users")     
def create_user(username: str, db: Session = Depends(get_db)):
    new_user = User(username=username, email="")
    db.add(new_user)
    db.commit()
    db.refresh(new_user)
    return {"id": new_user.id, "username": new_user.username, "email": new_user.email}

# Create empty hunt 
@app.post("/create-hunt")
def create_hunt(user_id: int, db: Session = Depends(get_db)):
    
    new_hunt = Hunt(
        name="Untitled Hunt",
        description="",
        place_to_play="",
        start_point="",
        created_by=user_id,
        is_active=False,
        private = False,
        created_at=datetime.utcnow()
    )
    db.add(new_hunt)
    db.commit()
    db.refresh(new_hunt)

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
def update_hunt(hunt_id: int, name: str = None, description: str = None, place_to_play: str = None, start_point: str = None, is_active: bool = None, private: bool = None, db: Session = Depends(get_db)):
    hunt = db.query(Hunt).filter(Hunt.id == hunt_id).first()
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



    db.commit()
    db.refresh(hunt)

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
def get_specific_hunt(hunt_id: int, db: Session = Depends(get_db)):
    hunt = db.query(Hunt).filter(Hunt.id == hunt_id).first()
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

# Get clue
@app.get("/hunts/{hunt_id}/clues/{clue_id}")
def get_specific_clue(hunt_id: int, clue_id: int, db: Session = Depends(get_db)):
    clue = db.query(Clue).filter(Clue.hunt_id == hunt_id, Clue.id == clue_id).first()
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
def update_clue(hunt_id: int, clue_id: int, title: str = None, description: str = None, hint: str = None, correct_answer: str = None, clue_order: int = None, image_url: str = None, audio_url: str = None, video_url: str = None, question_type: str = None, answer_type: str = None, choices: str = None, expected_gps: str = None, gps_radius: float = None, db: Session = Depends(get_db)):
    clue = db.query(Clue).filter(Clue.hunt_id == hunt_id, Clue.id == clue_id).first()
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

    db.commit()
    db.refresh(clue)

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
def create_empty_clue(hunt_id: int, db: Session = Depends(get_db)):

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
    db.commit()
    db.refresh(new_clue)

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
def join_hunt(hunt_id: int, user_id: int, db: Session = Depends(get_db)):
    # Check if the hunt exists
    hunt = db.query(Hunt).filter(Hunt.id == hunt_id).first()
    if not hunt:
        return {"error": "Hunt not found"}

    # Add user to the hunt
    user_hunt_progress = UserHuntProgress(
        user_id=user_id,
        hunt_id=hunt_id,
        current_clue_id=None,
        finished_at=None
    )
    db.add(user_hunt_progress)
    db.commit()
    db.refresh(user_hunt_progress)

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
def remove_user_from_hunt(hunt_id: int, user_id: int, db: Session = Depends(get_db)):
    # Check if the user is part of the hunt
    user_hunt_progress = db.query(UserHuntProgress).filter(
        UserHuntProgress.hunt_id == hunt_id, UserHuntProgress.user_id == user_id
    ).first()

    if not user_hunt_progress:
        return {"error": "User is not part of this hunt"}

    # Remove the user from the hunt
    db.delete(user_hunt_progress)
    db.commit()

    return {"message": "User successfully removed from the hunt"}

# Start hunt with skipping solved clues and return all current clue details
@app.post("/start-hunt/{hunt_id}")
def start_hunt(hunt_id: int, user_id: int, db: Session = Depends(get_db)):
    # Check if the hunt exists
    hunt = db.query(Hunt).filter(Hunt.id == hunt_id).first()
    if not hunt:
        return {"error": "Hunt not found"}

    # Check if the user is part of the hunt
    user_hunt_progress = db.query(UserHuntProgress).filter(
        UserHuntProgress.hunt_id == hunt_id, UserHuntProgress.user_id == user_id
    ).first()

    if not user_hunt_progress:
        return {"error": "User is not part of this hunt"}

    # Get all clues for the hunt, ordered by clue_order
    clues = db.query(Clue).filter(Clue.hunt_id == hunt_id).order_by(Clue.clue_order).all()
    if not clues:
        return {"error": "No clues available for this hunt"}

    # Find the first unsolved clue
    for clue in clues:
        solved = db.query(UserClueProgress).filter(
            UserClueProgress.user_id == user_id,
            UserClueProgress.clue_id == clue.id,
            UserClueProgress.is_solved == True
        ).first()
        if not solved:
            # Update the user's progress to the first unsolved clue
            user_hunt_progress.current_clue_id = clue.id
            db.commit()
            db.refresh(user_hunt_progress)

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
def check_if_answer_true(clue_id: int, user_id: int, answer: str, db: Session = Depends(get_db)):
    # Get the clue
    clue = db.query(Clue).filter(Clue.id == clue_id).first()
    if not clue:
        return {"error": "Clue not found"}

    # Check if the answer is correct
    if clue.correct_answer.lower() == answer.lower():
        # Mark the clue as solved for the user
        user_clue_progress = db.query(UserClueProgress).filter(
            UserClueProgress.user_id == user_id,
            UserClueProgress.clue_id == clue_id
        ).first()

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

        db.commit()
        db.refresh(user_clue_progress)

        return {"message": "Correct answer", "is_correct": True}

    return {"message": "Incorrect answer", "is_correct": False}


