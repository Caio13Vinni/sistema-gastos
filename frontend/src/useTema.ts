import { useEffect, useState } from "react";

type Tema = "claro" | "escuro";

const CHAVE_ARMAZENAMENTO = "sistema-gastos:tema";

function temaPreferidoDoSistema(): Tema {
  const prefereEscuro = window.matchMedia?.("(prefers-color-scheme: dark)").matches;
  return prefereEscuro ? "escuro" : "claro";
}


export function useTema() {
  const [tema, setTema] = useState<Tema>(() => {
    const salvo = localStorage.getItem(CHAVE_ARMAZENAMENTO);
    return salvo === "claro" || salvo === "escuro" ? salvo : temaPreferidoDoSistema();
  });

  useEffect(() => {
    document.documentElement.setAttribute("data-tema", tema);
    localStorage.setItem(CHAVE_ARMAZENAMENTO, tema);
  }, [tema]);

  function alternar() {
    setTema((atual) => (atual === "claro" ? "escuro" : "claro"));
  }

  return { tema, alternar };
}