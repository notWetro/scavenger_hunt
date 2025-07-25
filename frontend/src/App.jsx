import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Profile from "./pages/Profile";
import Hunts from "./pages/Hunts";
import Navbar from "./components/Navbar";
import Join from "./pages/JoinAndPlay/Join";
import StartHunt from "./pages/JoinAndPlay/StartHunt";
import EditHunt from "./pages/EditHunt/EditHunt";
import EditQuestion from "./pages/EditHunt/EditQuestion";
import "./App.css";
import Login from "./pages/Login/login";
import Register from "./pages/Login/register";

export default function App() {
  return (
    <Router>
      <div style={{ paddingBottom: "60px" }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/hunts" element={<Hunts />} />
          <Route path="/join" element={<Join />} />
          <Route path="/starthunt/:huntId" element={<StartHunt />} />
          <Route path="/edithunt" element={<EditHunt />} />
          <Route path="/editquestion" element={<EditQuestion />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Routes>
      </div>
      <Navbar />
    </Router>
  );
}
