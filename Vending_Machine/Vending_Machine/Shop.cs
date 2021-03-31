using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine
{
    public class Shop
    {
        // shopCart: 100 rows and 4 columns; Column 1 = userName, 2 = articleName, 3 = noItems, 4 = totalPrice
        static int maxShopRow = 100;
        static string[,] shopCart = new string [maxShopRow, 4];
        
        // BuyArticle performs a buy if conditions are OK
        // The functions returns the "buy amount" if OK, otherwise returns 0; 
        public int BuyArticle(string userName, string articleName, int noItems, int shopAmount)
        {
            int i, currentStock, articlePrice, currentNoItems, currentTotalPrice, newNoItems, newTotalPrice;
            bool buyOK = false;
            var message = new MessageToUser();
            var inventory = new Inventory();
            
            // Check first if article exist
            if (inventory.ArticleExist(articleName)==false)
            {
                message.Write("There is no article with that name!");
                return 0;
            }
            // Check if no of articles are available in inventory (stock); If not - notify user and return
            currentStock = inventory.QuantityInStock(articleName);
            if (currentStock <= noItems)
            {
                message.Write("There are not enough articles in Stock!");
                return 0;
            }
            
            // Check if user has enough money to buy; If not - notify user and return
            articlePrice = inventory.ArticlePrice(articleName);
            if (shopAmount < articlePrice*noItems)
            {
                message.Write("You have not enough money to buy this quantity!");
                return 0;
            }

            // Add article to shopCart 
            for (i = 0; i < maxShopRow; i++)
            {
                // Check if article already exist and then add total quantity and price
                if (shopCart[i, 0] == userName && shopCart[i,1] == articleName)
                {
                    currentNoItems = int.Parse(shopCart[i,2]);
                    currentTotalPrice = int.Parse(shopCart[i,3]);
                    newNoItems = currentNoItems + noItems;
                    newTotalPrice = currentTotalPrice + noItems*articlePrice;
                    shopCart[i, 2] = newNoItems.ToString();
                    shopCart[i, 3] = newTotalPrice.ToString();
                    buyOK = true;
                    break;
                }
                if (String.IsNullOrEmpty(shopCart[i, 0]))
                {
                    // Next available empty row is found
                    shopCart[i, 0] = userName;
                    shopCart[i, 1] = articleName;
                    shopCart[i, 2] = noItems.ToString();
                    newTotalPrice = noItems * articlePrice;
                    shopCart[i, 3] = newTotalPrice.ToString();
                    buyOK = true;
                    break;
                }
            }

            // Reduce quantity in inventory if successful buy
            if (buyOK == true)
            {
                inventory.ChangeQuantity(articleName, -noItems);
            }
            return (noItems * articlePrice);
        }
        
        public void PrintShopCart()
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxShopRow; i++)
            {
                // End loop if no more articles
                if (String.IsNullOrEmpty(shopCart[i, 0]))
                {
                    // If there was something to print, then wait for user input before break
                    if (i > 0)
                    {
                        message.Write("End of list!");
                    }
                    // If there are no items in shopCart, inform user
                    else if (i == 0)
                    {
                        message.Write("There are no items in your shopping cart!");
                    }
                    break;
                }
                
                // Print the shopcart info
                if (i == 0)
                {
                    // Write header before printing first shopcart line
                    Console.WriteLine("{0,-30} {1,-15} {2,-10}\n", "Article name","No of items", "Total price");
                }
                Console.WriteLine("{0,-30} {1,-15} {2,-10}", shopCart[i, 1], shopCart[i, 2], shopCart[i, 3]);
            }
            return;
        }
    }
}