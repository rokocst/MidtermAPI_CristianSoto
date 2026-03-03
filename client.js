// For local testing, use the port from your terminal (5059)
// When you deploy to Azure, replace this with your .azurewebsites.net URL
const API_URL = "http://localhost:5059"; 
const API_KEY = "MIDTERM_KEY_123";

async function runMidtermTests() {
    console.log("--- Starting Midterm API Tests ---");

    try {
        // 1. Call GET /usage (Part D)
        console.log("\nTesting GET /usage...");
        const usageResponse = await fetch(`${API_URL}/usage`, {
            headers: { "X-Api-Key": API_KEY }
        });

        if (!usageResponse.ok) {
            console.error(`Usage Error: ${usageResponse.status} ${usageResponse.statusText}`);
        } else {
            const usageData = await usageResponse.json();
            console.log("Usage Result:", usageData);
        }

        // 2. Call POST /items (Part A & E)
        console.log("\nTesting POST /items...");
        const newItem = {
            name: "Midterm Laptop",
            quantity: 5
        };

        const postResponse = await fetch(`${API_URL}/items`, {
            method: "POST",
            headers: { 
                "X-Api-Key": API_KEY,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newItem)
        });

        const postData = await postResponse.json();
        console.log("Post Result:", postData);

    } catch (error) {
        console.error("Test failed:", error.message);
        console.log("\nTroubleshooting Tips:");
        console.log("1. Ensure your .NET API is running in another terminal (dotnet run).");
        console.log(`2. Verify the API is listening on ${API_URL}`);
    }
}

runMidtermTests();