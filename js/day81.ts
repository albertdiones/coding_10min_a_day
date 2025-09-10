
const readline = require('readline');

const rl: any = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

let cardNumber: string = "";
rl.question('Please input your card number: ', 
    (answer: string): void => {
    cardNumber: string = answer;
    if (cardNumber.match(/^\d{16}$/)) {
        console.log("Your card number is valid"); 
    }
    else {
        console.error("Your card number is invalid!!!!");
    }
    rl.close(); 
});
