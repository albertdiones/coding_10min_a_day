
const readline = require('readline');

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

let cardNumber = "";
rl.question('Please input your card number: ', (answer) => {
  cardNumber = answer;
  

    if (cardNumber.match(/^\d{16}$/)) {
    console.log("Your card number is valid"); 
    }
    else {
        console.error("Your card number is invalid!!!!");
    }
    rl.close(); 
});
