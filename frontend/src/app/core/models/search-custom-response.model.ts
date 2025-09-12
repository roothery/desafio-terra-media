export interface SearchCustomResponse {
  docs: DocsResponse[];
  total: number;
}

export interface DocsResponse {
  titulo: string;
  autor: string[];
  autorFormatado: string;
  codigoAutor: string;
  anoPublicacao: string;
  codigoCapa: string;
  capaId: string;
}
