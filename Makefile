PROJECT_NAME=WebApi
STARTUP_PROJECT=$(PROJECT_NAME).Api
INFRA_PROJECT=$(PROJECT_NAME).Infrastructure

# Nama migration dikirim sebagai argumen: make migrate name=NamaMigration
migrate:
	dotnet ef migrations add ${name} -p $(INFRA_PROJECT) -s $(STARTUP_PROJECT)

update-db:
	dotnet ef database update -p $(INFRA_PROJECT) -s $(STARTUP_PROJECT)

run:
	dotnet run --project $(STARTUP_PROJECT)

add-package:
	dotnet add $(PROJECT_NAME).${lib} package ${package}

watch:
	dotnet watch --project $(STARTUP_PROJECT)

clean:
	dotnet clean

build:
	dotnet build

restore:
	dotnet restore

reset-db:
	dotnet ef database drop -p $(INFRA_PROJECT) -s $(STARTUP_PROJECT) --force --no-build
	dotnet ef database update -p $(INFRA_PROJECT) -s $(STARTUP_PROJECT)
# Menghapus migration terakhir (jika belum di-update): `make rollback`
rollback:
	dotnet ef migrations remove -p $(INFRA_PROJECT) -s $(STARTUP_PROJECT)


