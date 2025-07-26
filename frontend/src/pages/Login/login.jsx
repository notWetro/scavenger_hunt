import React, { useState, useContext } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../AuthContext";
import "./login.css";

export default function Login() {
  const { t } = useTranslation();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleSubmit = async e => {
    e.preventDefault();

    const form = new URLSearchParams();
    form.append('username', email);
    form.append('password', password);

    const res = await fetch("http://localhost:8000/auth/jwt/login", {
      method: "POST",
      headers: { "Content-Type": "application/x-www-form-urlencoded" },
      body: form.toString(),
    });
    const data = await res.json();
    if (res.ok) {
        console.log("Login successful:", data);
      login(data.access_token);
      navigate("/profile");
    } else {
        console.error("Login failed:", data);
      alert("Login failed");
    }
  };

  return (
    <div className="login-container">
      <form onSubmit={handleSubmit}>
        <h1>{t("login")}</h1>
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
          placeholder={t("password")}
          value={password}
          onChange={e => setPassword(e.target.value)}
        />
        <button className="main-button main-button-green" type="submit">{t("login")}</button>
      </form>
    </div>
  );
}