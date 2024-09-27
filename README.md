## Frontend
![CI Frontend](https://github.com/a1unade/YouTube.NET/actions/workflows/frontend-ci.yml/badge.svg)
[![Codecov](https://codecov.io/gh/a1unade/YouTube.NET/branch/main/graph/badge.svg?flag=frontend)](https://codecov.io/gh/a1unade/YouTube.NET)

## Postgres Docker

```bash
docker run --name youtube -p 5432:5432 -e POSTGRES_PASSWORD=youtube -d postgres:13.3
```