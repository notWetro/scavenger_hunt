import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Profile from "./pages/Profile";
import Hunts from "./pages/Hunts";
import Navbar from "./components/Navbar";
import "./App.css";

export default function App() {
  return (
    <Router>
      <div style={{ paddingBottom: "60px" }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/hunts" element={<Hunts />} />
        </Routes>
      </div>
      <Navbar />
    </Router>
  );
}
