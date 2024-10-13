docker:  
		docker-compose -f frontend/docker-compose.yml down --rmi all -v
		docker-compose -f frontend/docker-compose.yml up --build -d
		docker-compose -f backend/Youtube/docker-compose.yml down --rmi all -v
		docker-compose -f backend/Youtube/docker-compose.yml up --build -d

tests:  
		cd frontend/youtube-frontend && npm install
		cd frontend/youtube-accounts && npm install
		cd frontend/youtube-frontend-tests && npm install && npm run test

		cd backend/Youtube && dotnet test --no-build --verbosity normal --collect:"XPlat Code Coverage" --settings ../../.github/coverlet.runsettings