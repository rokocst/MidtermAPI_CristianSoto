cat <<EOF > README.txt
Project Name: MidtermAPI_CristianSoto
Student Name: Cristian Soto
Program: Computer Programming and Analysis

Description:
This project is a .NET 9 Web API developed for the Midterm Practical Examination. It implements a validated Item model, custom exception handling middleware, and API key security.

Completed Requirements:
- Part A: Item model with validation (Name/Quantity) and GET/POST endpoints.
- Part B: Custom Exception Middleware with 'fail=true' query parameter trigger.
- Part C: API Key Security Middleware protecting all endpoints with X-Api-Key.
- Part D: Stateful Request Counter using a thread-safe ConcurrentDictionary.
- Part E: Node.js client script for automated testing of usage and item creation.

Deployment:
- API deployed to Azure App Service.
- Source code hosted on private GitHub repository: MidtermAPI_CristianSoto.

Instructions to run client:
1. Ensure Node.js is installed.
2. Update the API_URL in client.js to the live Azure URL.
3. Run 'node client.js' in the terminal.
EOF