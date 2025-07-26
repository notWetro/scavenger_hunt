import React, { createContext, useState, useEffect } from "react";

export const AuthContext = createContext({
  token: null,
  user: null,
  login: () => {},
  logout: () => {},
  authFetch: (url, opts) => Promise.reject("No authFetch"),
});

const API_BASE = "http://localhost:8000";

export function AuthProvider({ children }) {
  const [token, setToken] = useState(() => localStorage.getItem("token"));
  const [user, setUser]   = useState(null);

  useEffect(() => {
    if (token) {
      localStorage.setItem("token", token);

      fetch(`${API_BASE}/users/me`, {
        headers: { Authorization: `Bearer ${token}` },
      })
        .then((res) => {
          if (!res.ok) throw new Error("Unauthorized");
          return res.json();
        })
        .then((data) => setUser(data))
        .catch(() => {
          setUser(null);
          setToken(null);
        });
    } else {
      localStorage.removeItem("token");
      setUser(null);
    }
  }, [token]);

  const login = (newToken) => setToken(newToken);
  const logout = () => setToken(null);

  const authFetch = (url, opts = {}) => {
    const headers = {
      ...(opts.headers || {}),
      Authorization: token ? `Bearer ${token}` : undefined,
    };
    return fetch(url.startsWith("http") ? url : API_BASE + url, {
      ...opts,
      headers,
    });
  };

  return (
    <AuthContext.Provider value={{ token, user, login, logout, authFetch }}>
      {children}
    </AuthContext.Provider>
  );
}
