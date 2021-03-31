using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine
{
    public class User
    {
        // userTable: 50 rows and 2 columns; Column 1 = userName, 2 = password
        static int maxUserRow = 50;
        static string[,] userTable = new string [maxUserRow, 2];
        static string adminUserName = "Admin"; 
        static string adminPassword = "Admin";
        public void AddUser(string userName, string password)
        {
            int i;
            var message = new MessageToUser();
            var bank = new Bank();

            for (i = 0; i < maxUserRow; i++)
            {
                // Check if username already exist and inform user and then interrupt
                if (userTable[i, 0] == userName)
                {
                    message.Write("User already exist! Please use another Username!");
                    break;
                }
                if (String.IsNullOrEmpty(userTable[i, 0]))
                {
                    // Next available empty row is found
                    userTable[i, 0] = userName;
                    userTable[i, 1] = password;
                    message.Write("User is now added!");
                    // Also add account with balance zero
                    bank.AddAccount(userName);
                    break;
                }
            }
            return;
        }

        public void DeleteUser(string userName)
        {
            int i, j, deleteRow;
            var message = new MessageToUser();

            for (i = 0; i < maxUserRow; i++)
            {
                // End loop if now more users
                if (String.IsNullOrEmpty(userTable[i, 0]))
                {
                    break;
                }

                if (userTable[i, 0] == userName)
                {
                    // User is found and will be deleted
                    deleteRow = i;
                    for (j = deleteRow + 1; j < maxUserRow; j++)
                    {
                        userTable[j - 1, 0] = userTable[j, 0];
                        userTable[j - 1, 1] = userTable[j, 1];
                        // Interrupt when there are no more users in the table 
                        if (String.IsNullOrEmpty(userTable[j, 0]))
                        {
                            break;
                        }
                    }
                    message.Write("User is now deleted!");
                    break;
                }
            }
            return;
        }
        
        public bool ValidateUser(string userName, string password)
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxUserRow; i++)
            {
                // End loop if no more users
                if (String.IsNullOrEmpty(userTable[i, 0]))
                {
                    message.Write("User not found!");
                    return false;
                }

                if (userTable[i, 0] == userName)
                {
                    // User is found, check if password is OK
                    if (userTable[i, 1] == password)
                    {
                        message.Write("Your are now logged in!");
                        return true;
                    }
                    else
                    {
                        message.Write("Password not correct!");
                        return false;
                    }
                }
            }
            return false;
        }
        
        public bool IsUserAdmin(string userName)
        {
            if ( userName == adminUserName )
                    return true;
            return false;
        } 
        public void PrintUsers()
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxUserRow; i++)
            {
                // End loop if no more users
                if (String.IsNullOrEmpty(userTable[i, 0]))
                {
                    // If there was something to print, then wait for user input before break
                    if (i > 0)
                    {
                        message.Write("End of list!");
                    }
                    break;
                }
                // Print the user info
                if (i == 0)
                {
                    // Write header before printing first user
                    Console.WriteLine("{0,-20} {1,-20}\n", "Username","Password");    
                }
                Console.WriteLine("{0,-20} {1,-20}", userTable[i, 0], userTable[i, 1]);
            }
            return;
        }
        public void CreateUsers()
        {
            userTable[0, 0] = adminUserName; userTable[0, 1] = adminPassword;
            userTable[1, 0] = "Rikard"; userTable[1, 1] = "Rikard";
            userTable[2, 0] = "Olle"; userTable[2, 1] = "Olle";
            return;
        }
    }
}