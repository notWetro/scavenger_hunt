import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import "./register.css";

export default function Register() {
  const { t } = useTranslation();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [username, setUsername] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async e => {
    e.preventDefault();
    const res = await fetch("http://localhost:8000/auth/register", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password, username }),
    });
    if (res.ok) {
      navigate("/login");
    } else {
      alert("Registration failed");
    }
  };

  return (
    <div className="register-container">
      <form onSubmit={handleSubmit}>
        <h1>{t("register")}</h1>
        <input
          className="input-field"
          type="email"
          placeholder={t("email")}
          value={email}
          onChange={e => setEmail(e.target.value)}
        />
        <input
          className="input-field"
          type="password"
          placeholder="Password"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />
        <input
          className="input-field"
          type="username"
          placeholder={t("username")}
          value={username}
          onChange={e => setUsername(e.target.value)}
        />
        <button className="main-button main-button-green" type="submit">{t("register")}</button>
      </form>
    </div>
  );
}