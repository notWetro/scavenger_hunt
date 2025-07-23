from fastapi import FastAPI, Depends
from sqlalchemy import (
    create_engine, Column, Integer, String, Boolean, Float, Text,
    ForeignKey, DateTime
)
from sqlalchemy.orm import sessionmaker, declarative_base, Session
from datetime import datetime

# === PostgreSQL DATABASE CONFIG ===
DATABASE_URL = "postgresql://postgres:1967@localhost:5432/scavengerhunt"


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




