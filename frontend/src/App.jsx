import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Profile from "./pages/Profile";
import Hunts from "./pages/Hunts";
import Navbar from "./components/Navbar";
import Join from "./pages/Join";
import Create from "./pages/Create";
import StartHunt from "./pages/StartHunt";
import "./App.css";

export default function App() {
  return (
    <Router>
      <div style={{ paddingBottom: "60px" }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/hunts" element={<Hunts />} />
          <Route path="/join" element={<Join />} />
          <Route path="/create" element={<Create />} />
          <Route path="/StartHunt/:huntId" element={<StartHunt />} />
        </Routes>
      </div>
      <Navbar />
    </Router>
  );
}
