using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine
{
    public class Bank
    {
        // bankTable: 50 rows and 2 columns; Column 1 = userName, 2 = userAmount
        static int maxBankRow = 50;
        static string[,] bankTable = new string [maxBankRow, 2];

        public void AddAccount(string userName)
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxBankRow; i++)
            {
                // Check if username already exist and interrupt in such case
                if (bankTable[i, 0] == userName)
                {
                    message.Write("No account created! The user has already an account!");
                    break;
                }

                // If user not exist, then add user and the amount in the end of the table
                if (String.IsNullOrEmpty(bankTable[i, 0]))
                {
                    // Next available empty row is found
                    bankTable[i, 0] = userName;
                    bankTable[i, 1] = "0";
                    message.Write("New account is now added!\n");
                    break;
                }
            }
            return;
        }
        
        public int AddAmount(string userName, int amount)
        {
            int i, balance;
            bool userExist = false;
            var message = new MessageToUser();

            for (i = 0; i < maxBankRow; i++)
            {
                // Check if username exist and then add amount to existing balance
                if (bankTable[i, 0] == userName)
                {
                    balance = int.Parse(bankTable[i,1]);
                    // If balance will be negative , no withdrawel is possible
                    if ((balance+amount) <= 0)
                    {
                        message.Write("There is not enough balance on your account! Balance = "+ bankTable[i,1] + "\n");
                        return 0;
                    }
                    
                    balance += amount;
                    bankTable[i, 1] = balance.ToString();
                    message.Write("New balance of the account is: "+ bankTable[i,1] + "\n");
                    userExist = true;
                    break;
                }
            }
            // If user is not found, then notify user
            if (userExist==false)
            {
                message.Write("No account exist with that Username!\n");
                amount = 0;
            }
            return Math.Abs(amount);
        }

        public void DeleteAccount(string userName)
        {
            int i, j, balance, deleteRow;
            var message = new MessageToUser();

            for (i = 0; i < maxBankRow; i++)
            {
                // End loop if now more accounts
                if (String.IsNullOrEmpty(bankTable[i, 0]))
                {
                    break;
                }
                if (bankTable[i, 0] == userName)
                {
                    // Account is found and will be deleted if balance is zero
                    balance = int.Parse(bankTable[i,1]);
                    if (balance != 0)
                    {
                        // Balance is not zero and account will not be deleted
                        message.Write("Account not deleted since balance is not zero!");
                        break;
                    }
                    // 
                    deleteRow = i;
                    for (j = deleteRow + 1; j < maxBankRow; j++)
                    {
                        bankTable[j - 1, 0] = bankTable[j, 0];
                        bankTable[j - 1, 1] = bankTable[j, 1];
                        // Interrupt when there are no more accounts in the table 
                        if (String.IsNullOrEmpty(bankTable[j, 0]))
                        {
                            break;
                        }
                    }
                    message.Write("Account is now deleted!");
                    break;
                }
            }
            return;
        }

        public void PrintAccounts()
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxBankRow; i++)
            {
                // End loop if no more articles
                if (String.IsNullOrEmpty(bankTable[i, 0]))
                {
                    // If there was something to print, then wait for user input before break
                    if (i > 0)
                    {
                        message.Write("End of list!");
                    }
                    break;
                }
                
                // Print the account info
                if (i == 0)
                {
                    // Write header before printing first account
                    Console.WriteLine("{0,-30} {1,-10}\n", "User", "Balance");
                }
                Console.WriteLine("{0,-30} {1,-10}", bankTable[i, 0], bankTable[i, 1]);
            }
            return;
        }
        
        public void CreateAccounts()
        {
            bankTable[0, 0] = "Rikard"; bankTable[0, 1] = "10000";
            bankTable[1, 0] = "Olle"; bankTable[1, 1] = "3000";
            bankTable[2, 0] = "Admin"; bankTable[2, 1] = "1000000";
            return;
        }
    }
}