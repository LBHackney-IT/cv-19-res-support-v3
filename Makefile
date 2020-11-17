.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build cv-19-res-support-api-v3

.PHONY: serve
serve:
	docker-compose build cv-19-res-support-api-v3 && docker-compose up cv-19-res-support-api-v3

.PHONY: migrate-dev-database
migrate-dev-database:
	-dotnet tool install -g dotnet-ef
	CONNECTION_STRING="Host=127.0.0.1;Port=5433;Username=postgres;Password=mypassword;Database=devdb" dotnet ef database update -p cv19ResSupportV3

.PHONY: shell
shell:
	docker-compose run cv-19-res-support-api-v3 bash

.PHONY: test
test:
	docker-compose up test-database & docker-compose build cv-19-res-support-api-v3-test && docker-compose up cv-19-res-support-api-v3-test

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format
