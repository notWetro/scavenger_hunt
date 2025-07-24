from fastapi_users import schemas

class UserCreate(schemas.BaseUserCreate):
    username: str

class UserRead(schemas.BaseUser[ int ]):
    username: str
    language: str
    dark_mode: bool

class UserUpdate(schemas.BaseUserUpdate):
    username: str
    language: str
    dark_mode: bool
