const apiUrl = 'https://localhost:44374';
let resultStatus; //boolean
let resultData;


const data = {
    Sum: 1,
    PaymentsNum: 1,
    Description: "The destination of the payment"
}

const options = {
    method: "POST",
    headers: {
        "Content-Type": "application/json"
    },
    body: JSON.stringify(data)
}

async function getPaymentLink() {
    const response = await fetch(`${apiUrl}/api/Payment/GetPaymentLink`, options); //response in format {isSuccess: boolean, message: string (link or error-message)}
    const data = await response.json();
    console.log(data.message);
    window.open(data.message);
}