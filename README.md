# 🎯 Scavenger Hunt

A modern web application for creating and playing scavenger hunts with geolocation features, audio/image support, and real-time progress tracking.

## 🚀 Features

### For Hunt Creators
- 🗺️ **Interactive Maps**: Create scavenger hunts with Leaflet map integration
- 🎵 **Multimedia Clues**: Add audio files and images to clues
- 📱 **QR Code Generation**: Automatic QR code creation for easy sharing
- 👥 **User Management**: Secure authentication with FastAPI-Users
- 📊 **Progress Tracking**: Monitor game progress in real-time

### For Players
- 🎮 **Intuitive Interface**: Modern React-based gaming interface
- 🌍 **Geolocation**: Location-based clues and verification
- 🎯 **Drag & Drop**: Easy interaction with @hello-pangea/dnd
- 🌐 **Multi-language**: i18next integration for international support
- 📱 **Mobile-Optimized**: Responsive design for all devices

## 🛠️ Tech Stack

### Backend
- **FastAPI** - Modern, fast Python web framework
- **PostgreSQL** - Robust relational database
- **SQLAlchemy** - ORM with async support
- **FastAPI-Users** - User authentication and management
- **Pydantic** - Data validation and serialization

### Frontend
- **React 19** - Modern UI library
- **Vite** - Fast build tool
- **React Router** - Client-side routing
- **Leaflet** - Interactive maps
- **React-QR-Code** - QR code generation

### Infrastructure
- **Docker & Docker Compose** - Containerization
- **Traefik** - Reverse proxy with automatic SSL certificates
- **Let's Encrypt** - Free SSL certificates

## 📦 Installation & Setup

### Prerequisites
- Docker & Docker Compose
- Node.js 18+ (for local development)
- Python 3.8+ (for local development)

### Production Deployment

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd scavenger_hunt
   ```

2. **Start production environment**
   ```bash
   docker-compose up -d
   ```

The application will be available at `https://werwoelfe.fun`.

### Local Development

1. **Start local development environment**
   ```bash
   docker-compose -f docker-compose-local.yaml up -d
   ```

2. **Backend development** (optional, for local API development)
   ```bash
   cd backend
   pip install -r requirements.txt
   uvicorn main:app --reload --host 0.0.0.0 --port 8000
   ```

3. **Frontend development** (optional, for local UI development)
   ```bash
   cd frontend
   npm install
   npm run dev
   ```

## 🔧 Configuration

### Environment Variables

**Backend:**
- `DATABASE_URL` - PostgreSQL connection string
- `SECRET` - JWT secret for authentication

**Frontend:**
- `VITE_API_BASE` - Backend API URL

### Docker Compose Services

- **traefik** - Reverse proxy (Port 80, 443)
- **db** - PostgreSQL database
- **backend** - FastAPI application
- **frontend** - React application (Vite build)

## 🗂️ Project Structure

```
scavenger_hunt/
├── backend/                 # FastAPI Backend
│   ├── main.py             # Main API file
│   ├── schemas.py          # Pydantic models
│   ├── requirements.txt    # Python dependencies
│   └── dockerfile          # Backend Docker image
├── frontend/               # React Frontend
│   ├── src/               # Source code
│   ├── public/            # Static assets
│   ├── package.json       # Node.js dependencies
│   └── Dockerfile         # Frontend Docker image
├── media/                 # Upload directory for media
├── docker-compose.yaml    # Production setup
└── docker-compose-local.yaml # Development setup
```

## 🎮 Usage

1. **Create Account** - Register on the platform
2. **Create Scavenger Hunt** - Use the hunt editor to create hunts
3. **Add Clues** - Add locations, texts, images, and audio
4. **Share** - Generate a QR code or share the hunt code
5. **Play** - Others can join the scavenger hunt using the code

## 🤝 Contributing

Contributions are welcome! Please create an issue or pull request.

## 📄 License

[Insert license here]

---

🎯 **Live Demo**: https://werwoelfe.fun
