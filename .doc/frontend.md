# Terra Média - Frontend

Aplicação construída em Angular 18 com componentes standalone, usando Angular Material para UI, Bootstrap 5 para layout, ngx-bootstrap para modais e lottie-web para animações. Os dados vêm da Open Library (sem necessidade de chave de API).

## Tecnologias

- **Angular 18, TypeScript 5, RxJS**
- **Angular Material:** table, paginator, sort, form-field, input, icon, menu, button
- **Bootstrap 5 + ngx-bootstrap** (BsModalService)
- **lottie-web** (animação na aba “Comentários”)
- **Jasmine + Karma** (testes unitários)

## 🗂️ Estrutura de Pastas

```
frontend/
├─ angular.json, package.json, tsconfig*.json
└─ src/
   ├─ assets/images/        # logos, capa fallback, animação lottie
   ├─ app/
   │  ├─ pages/home/        # página principal (título, subtítulo, <app-livros>)
   │  ├─ components/
   │  │  ├─ menu-sidebar/   # header lateral
   │  │  ├─ livros/         # tabela com filtro/sort/paginação + modais
   │  │  ├─ modal-autor/    # propriedades do autor
   │  │  └─ modal-livro/    # propriedades do livro + lottie
   │  └─ core/              # services, models, enums, paginator intl
   ├─ styles.scss
   └─ main.ts, app.config.ts, app.routes.ts
```

## Pré-requisitos

- **Node.js 18+** (LTS recomendado)
- **Angular CLI 18** (opcional): `npm i -g @angular/cli`

## Como rodar

```bash
cd frontend
npm install
npm start     # ou: ng serve
# Acesse: http://localhost:4200/
```

## Testes

```bash
cd frontend
npm test
ng test --watch=false --browsers=ChromeHeadless   # execução única/headless (CI)
```

## Dicas rápidas

- O filtro da tabela busca por **título** e **autor** (case-insensitive).
- A imagem da capa tem fallback em `assets/images/livro-nao-encontrado.png`.
- Biografias e capas são obtidas via `OpenLibraryService`.
