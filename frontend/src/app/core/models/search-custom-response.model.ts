export interface SearchCustomResponse {
  docs: DocsResponse[];
  total: number;
}

export interface DocsResponse {
  titulo: string;
  autor: string[];
  codigoAutor: string;
  anoPublicacao: string;
  codigoCapa: string;
  capaId: string;
}
