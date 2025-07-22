import { useTranslation } from "react-i18next";
import "./Home.css";

export default function Home() {
  const { t } = useTranslation();
  return (
    <div style={{ textAlign: "center", marginTop: "50px" }}>
      <h1>{t("welcome")}</h1>
    </div>
  );
}
