import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate, useLocation } from "react-router-dom";
import { FaHome, FaUser, FaCrosshairs } from "react-icons/fa";
import "./Navbar.css";

export default function Navbar() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const location = useLocation();

  const currentTab =
    location.pathname === "/profile"
      ? "profile"
      : location.pathname === "/hunts"
      ? "hunts"
      : "home";

  const [selected, setSelected] = useState(currentTab);

  const handleChange = (tab) => {
    setSelected(tab);
    if (tab === "home") navigate("/");
    if (tab === "profile") navigate("/profile");
    if (tab === "hunts") navigate("/hunts");
  };

  return (
    <nav className="navbar-fixed">
      <div className="glass-radio-group">
        {/* Hunts */}
        <input
          type="radio"
          id="tab-hunts"
          name="glass"
          checked={selected === "hunts"}
          onChange={() => handleChange("hunts")}
        />
        <label htmlFor="tab-hunts" data-label={t("hunts")}>
          <FaCrosshairs size={22} />
        </label>

        {/* Home */}
        <input
          type="radio"
          id="tab-home"
          name="glass"
          checked={selected === "home"}
          onChange={() => handleChange("home")}
        />
        <label htmlFor="tab-home" data-label={t("home")}>
          <FaHome size={22} />
        </label>

        {/* Profile */}
        <input
          type="radio"
          id="tab-profile"
          name="glass"
          checked={selected === "profile"}
          onChange={() => handleChange("profile")}
        />
        <label htmlFor="tab-profile" data-label={t("profile")}>
          <FaUser size={22} />
        </label>

        <span className="glass-glider" />
      </div>
    </nav>
  );
}
