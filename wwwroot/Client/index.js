const apiUrl = 'https://localhost:44374';
let resultStatus; //boolean
let resultData;


const data = {
    Sum: 1,
    PaymentsNum: 1,
    Description: "The destination of the payment",
    Name: "John Smit",
    Phone: "0500000000"
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

window.addEventListener("growWalletChange", (result) => {
    console.log("result:", result.detail);
    let res = result.detail;
    if (res.status === 1) {
        resultStatus = true;
        resultData = res.data;
    }
    if (res.state === "close" && resultStatus) {
        console.log(resultData) //or display success page with resultData
        window.location.href = "/success"
    }
});