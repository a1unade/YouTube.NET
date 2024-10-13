## CI status
![Frontend CI](https://github.com/a1unade/YouTube.NET/actions/workflows/frontend-ci.yml/badge.svg)
[![Codecov](https://codecov.io/gh/a1unade/YouTube.NET/branch/main/graph/badge.svg?flag=frontend)](https://codecov.io/gh/a1unade/YouTube.NET)

![Backend CI](https://github.com/a1unade/YouTube.NET/actions/workflows/backend-ci.yml/badge.svg)
[![Codecov](https://codecov.io/gh/a1unade/YouTube.NET/branch/main/graph/badge.svg?flag=backend)](https://codecov.io/gh/a1unade/YouTube.NET)

## Как запускать проект в Docker

В корневой папке нужно ввести команду ```make docker```, чтобы поднять все контейнеры.

В результате должен быть примерно такой вывод: 

<img src="./materials//7.png">

## Запуск тестов для всего проекта

В корневой папке нужно ввести команду ```make tests```. Запустятся тесты как для фронтенда, так и для бэкенда.

В консоли должен появиться такой вывод с результатми тестов:

<img src="./materials//8.png">