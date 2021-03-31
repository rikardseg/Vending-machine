using System;

namespace Vending_Machine
{
    class Program
    {
        public static int shopAmount = 0;
        
        static void Main(string[] args)
        {
            var choice = "";
            string userName ="", password ="";
            var message = new MessageToUser();
            var inventory = new Inventory();
            var user = new User();
            var bank = new Bank();
            
            // Create list of a) Users and b) Articles and c) Bank accounts for users
            user.CreateUsers();
            inventory.CreateArticles();
            bank.CreateAccounts();
            
            //Login
            LoginUser(ref userName, ref password);

            // Write main menu and wait for input
            while (choice == "")
            {
                ClearConsole();
               
                Console.WriteLine("Welcome to Rikard's Vending Machine ({0})\n", userName);
                Console.WriteLine("Main menu\n");
                Console.WriteLine("A - Shop");
                Console.WriteLine("B - Bank");
                Console.WriteLine("C - Inventory");
                Console.WriteLine("D - User handling");
                Console.WriteLine("E - Change user");
                Console.WriteLine("F - Exit");
                Console.WriteLine("\nWhat do you want to do?");
                Console.Write("Select A, B, C, D or E: ");

                choice = Console.ReadLine();
                choice = choice.ToUpper();

                switch (choice) { 
                    case "A":
                        Shopping(userName);
                        break; 
                    case "B":
                        BankHandling(userName);
                        break;
                    case "C":
                        if (user.IsUserAdmin(userName) == true)
                            InventoryHandling(userName);
                        else
                            message.Write("You do not have access to this function.");
                        break;
                    case "D":
                        if (user.IsUserAdmin(userName) == true)
                            UserHandling(userName);
                        else
                            message.Write("You do not have access to this function.");
                        break;
                    case "E":
                        LoginUser(ref userName, ref password);
                        break;
                    case "F":
                        ClearConsole();
                        message.Write("Thanks for shopping at Rikard's Vending Machine!\nSee you soon and Welcome back!");
                        Environment.Exit(0);
                        break;
                    default:
                        message.Write("Wrong option selected - Please try again!");
                        break; 
                } 
                choice = "";
            }
        }

        private static void Shopping(string currentUserName)
        {
            var choice = "";
            string articleName, inputNoItems;
            int noItems, buyAmount;
            var shop = new Shop();
            var inventory = new Inventory();
            var message = new MessageToUser();
            
            while (choice == "")
            {
                ClearConsole();
                Console.WriteLine("Shopping menu ({0})\n", currentUserName);
                Console.WriteLine("A - Show list of articles I can buy");
                Console.WriteLine("B - Buy article");
                Console.WriteLine("C - Show my Shopping cart");
                Console.WriteLine("D - Back to Main menu");
                Console.WriteLine("\nWhat do you want to do?");
                Console.Write("Select A, B, C or D: ");

                choice = Console.ReadLine();
                choice = choice.ToUpper();

                switch (choice) { 
                    case "A":
                        inventory.PrintArticles();
                        break; 
                    case "B":
                        Console.Write("What article do you want to buy: ?");
                        articleName = Console.ReadLine();
                        // Check first if article exist
                        if (inventory.ArticleExist(articleName)==false)
                        {
                            message.Write("There is no article with that name!");
                            break;
                        }
                        Console.Write("How many items do you want to buy: ?");
                        inputNoItems = Console.ReadLine();
                        // If nothing was entered in noItems, inform user
                        if (String.IsNullOrEmpty(inputNoItems))
                        {
                            message.Write("No value was entered!");
                        }
                        // If no money, inform user 
                        else if (shopAmount == 0)
                        {
                            message.Write("You have no money! Please withdraw from bank!");
                        }
                        else
                        {
                            // Try to execute the shopping request
                            noItems = int.Parse(inputNoItems);
                            buyAmount = shop.BuyArticle(currentUserName, articleName, noItems, shopAmount);
                            // If shopping is successful then reduce shopAmount with the "buy amount"
                            if (buyAmount != 0)
                            {
                                shopAmount = shopAmount - buyAmount;
                            }
                        }
                        break;
                    case "C":
                        shop.PrintShopCart();
                        message.Write("Available amount for shopping = " + shopAmount);
                        break;
                    case "D":
                        // Back to main menu
                        return;
                    default:
                        message.Write("Wrong option selected - Please try again!");
                        break; 
                } 
                choice = "";
            }
        }
        private static void BankHandling( string currentUserName )
        {
            var choice = "";
            var bank = new Bank();
            var user = new User();
            var message = new MessageToUser();
            string addUserName, amount;
            
            while (choice == "")
            {
                ClearConsole();
                Console.WriteLine("Bank handling menu ({0})\n", currentUserName);
                Console.WriteLine("A - Add an account");
                Console.WriteLine("B - Add money to account");
                Console.WriteLine("C - Withdraw money from account");
                Console.WriteLine("D - Print all user accounts");
                Console.WriteLine("E - Back to Main menu");
                Console.WriteLine("\nWhat do you want to do?");
                Console.Write("Select A, B, C, D or E: ");

                choice = Console.ReadLine();
                choice = choice.ToUpper();

                switch (choice) { 
                    case "A":
                        // Ask for username 
                        Console.Write("Type Username to add account for: ? ");
                        addUserName = Console.ReadLine();
                        // Add an account for this user
                        bank.AddAccount(addUserName);
                        break; 
                    case "B":
                        Console.WriteLine("Type the amount to add: ?");
                        amount = Console.ReadLine();
                        // If nothing was entered the inform user else add amount
                        if (String.IsNullOrEmpty(amount))
                        {
                            message.Write("No amount was entered!");
                        }
                        else
                        {
                            // Add the amount to the account for this user
                            bank.AddAmount(currentUserName, int.Parse(amount));
                        }
                        break;
                    case "C":
                        Console.Write("Type the amount you want to withdraw: ? ");
                        amount = Console.ReadLine();
                        // If nothing was entered the inform user else withdraw amount
                        if (String.IsNullOrEmpty(amount))
                        {
                            message.Write("No amount was entered!");
                        }
                        else
                        {
                            // Withdraw the amount from the account for this user
                            // And set the shopAmount that the user can shop for
                            shopAmount = bank.AddAmount(currentUserName, -(int.Parse(amount)));
                        }
                        break;
                    case "D":
                        bank.PrintAccounts();
                        break;
                    case "E":
                        // Back to main menu
                        return;
                    default:
                        message.Write("Wrong option selected - Please try again!");
                        break; 
                } 
                choice = "";
            }
        }

        private static void InventoryHandling( string currentUserName )
        {
            var choice = "";
            var inventory = new Inventory();
            var message = new MessageToUser();
            string articleName, articlePrice, articleQuantity;
            
            while (choice == "")
            {
                ClearConsole();
                Console.WriteLine("Inventory handling menu ({0})\n", currentUserName);
                Console.WriteLine("A - Add an article");
                Console.WriteLine("B - Delete an article");
                Console.WriteLine("C - Print all articles");
                Console.WriteLine("D - Back to Main menu");
                Console.WriteLine("\nWhat do you want to do?");
                Console.Write("Select A, B, C or D: ");

                choice = Console.ReadLine();
                choice = choice.ToUpper();

                switch (choice) { 
                    case "A":
                        // Ask for name, price and quantity of an article to be added
                        Console.Write("Type name of article: ? ");
                        articleName = Console.ReadLine();
                        Console.Write("Type price of article: ? ");
                        articlePrice = Console.ReadLine();
                        Console.Write("Type quantity of article: ? ");
                        articleQuantity = Console.ReadLine();
                        // Add user to table
                        inventory.AddArticle(articleName, articlePrice, articleQuantity);
                        break; 
                    case "B":
                        // Ask for article to delete
                        Console.Write("Type name of article you want to delete: ? ");
                        articleName = Console.ReadLine();
                        // Delete user from table
                        inventory.DeleteArticle(articleName);
                        break;
                    case "C":
                        inventory.PrintArticles();                        
                        break;
                    case "D":
                        // Back to main menu
                        return;
                    default:
                        message.Write("Wrong option selected - Please try again!");
                        break; 
                } 
                choice = "";
            }
        }
        
        private static void UserHandling( string currentUserName )
        {
            var choice = "";
            var user = new User();
            var message = new MessageToUser();
            string userName, password;
            
            while (choice == "")
            {
                ClearConsole();
                Console.WriteLine("User handling menu ({0})\n", currentUserName);
                Console.WriteLine("A - Add a user");
                Console.WriteLine("B - Delete a user");
                Console.WriteLine("C - Print all users");
                Console.WriteLine("D - Back to Main menu");
                Console.WriteLine("\nWhat do you want to do?");
                Console.Write("Select A, B, C or D: ");

                choice = Console.ReadLine();
                choice = choice.ToUpper();

                switch (choice) { 
                    case "A":
                        // Ask for username and password to add
                        Console.Write("Type Username you want to add: ? ");
                        userName = Console.ReadLine();
                        Console.Write("Type Password: ? ");
                        password = Console.ReadLine();
                        // Add user to table
                        user.AddUser(userName, password);
                        break; 
                    case "B":
                        // Ask for username to delete
                        Console.Write("Type the Username you want to delete: ? ");
                        userName = Console.ReadLine();
                        // Delete user from table
                        user.DeleteUser(userName);
                        break;
                    case "C":
                        user.PrintUsers();                        
                        break;
                    case "D":
                        // Back to main menu
                        return;
                    default:
                        message.Write("Wrong option selected - Please try again!");
                        break; 
                } 
                choice = "";
            }
        }
        private static void ClearConsole()
        {
            ConsoleColor foreColor = Console.ForegroundColor;
            ConsoleColor backColor = Console.BackgroundColor;
            Console.Clear();
        }
        // Log in of user; Request userName and password
        private static void LoginUser(ref string userName, ref string password)
        {
            bool loginOK = false;
            var user = new User();
            
            while (loginOK == false)
            {
                ClearConsole();
                Console.Write("Type your Username: ? ");
                userName = Console.ReadLine();
                Console.Write("Type your Password: ? ");
                password = Console.ReadLine();
                // If userName and password is OK then break this while loop, otherwise continue to ask
                loginOK = user.ValidateUser(userName, password);
            }
        }
    }
}
