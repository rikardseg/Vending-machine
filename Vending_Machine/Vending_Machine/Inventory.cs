using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine
{
    public class Inventory
    {
        // inventoryTable: 100 rows and 3 columns; Column 1 = articleName, 2 = articlePrice, 3 = articleQuantity
        static int maxInventoryRow = 100;
        static string[,] inventoryTable = new string [maxInventoryRow, 3];
        public void AddArticle(string articleName, string articlePrice, string articleQuantity)
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxInventoryRow; i++)
            {
                // Check if article name already exist and inform user and then interrupt
                if (inventoryTable[i, 0] == articleName)
                {
                    message.Write("Article already exist!");
                    break;
                }
                if (String.IsNullOrEmpty(inventoryTable[i, 0]))
                {
                    // Next available empty row is found
                    inventoryTable[i, 0] = articleName;
                    inventoryTable[i, 1] = articlePrice;
                    inventoryTable[i, 2] = articleQuantity;
                    message.Write("Article is now added!");
                    break;
                }
            }
            return;
        }

        public void DeleteArticle(string articleName)
        {
            int i, j, deleteRow;
            var message = new MessageToUser();

            for (i = 0; i < maxInventoryRow; i++)
            {
                // End loop if no more articles
                if (String.IsNullOrEmpty(inventoryTable[i, 0]))
                {
                    break;
                }

                if (inventoryTable[i, 0] == articleName)
                {
                    // Article is found and will be deleted
                    deleteRow = i;
                    for (j = deleteRow + 1; j < maxInventoryRow; j++)
                    {
                        inventoryTable[j - 1, 0] = inventoryTable[j, 0];
                        inventoryTable[j - 1, 1] = inventoryTable[j, 1];
                        inventoryTable[j - 1, 2] = inventoryTable[j, 2];
                        // Interrupt when there are no more articles in the table 
                        if (String.IsNullOrEmpty(inventoryTable[j, 0]))
                        {
                            break;
                        }
                    }
                    message.Write("Article is now deleted!");
                    break;
                }
            }
            return;
        }
        public bool ArticleExist(string articleName)
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxInventoryRow; i++)
            {
                // Check if article exist
                if (inventoryTable[i, 0] == articleName)
                    return true;
            }
            // Article does not exist and then return false
            return false;
        }

        public int QuantityInStock(string articleName)
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxInventoryRow; i++)
            {
                // Check if article exist and if there is enough items in stock
                if (inventoryTable[i, 0] == articleName)
                {
                    return int.Parse(inventoryTable[i,2]);
                }
            }
            // Article does not exist; Notify user and return 0
            message.Write("Article does not exist!");
            return 0;
        }
        public bool ChangeQuantity(string articleName, int noItems)
        {
            int i, currentQuantity, newQuantity;
            var message = new MessageToUser();

            for (i = 0; i < maxInventoryRow; i++)
            {
                // Check if article exist and adjust quantity if found
                if (inventoryTable[i, 0] == articleName)
                {
                    currentQuantity = int.Parse(inventoryTable[i, 2]);
                    newQuantity = currentQuantity + noItems;
                    inventoryTable[i, 2] = newQuantity.ToString(); 
                   return true;
                }
            }
            return false;
        } 
        public int ArticlePrice(string articleName)
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxInventoryRow; i++)
            {
                // Check if article exist and if there is enough items in stock
                if (inventoryTable[i,0] == articleName)
                {
                    return int.Parse(inventoryTable[i,1]);
                }
            }
            // Article does not exist; Notify user and return 0
            message.Write("Article does not exist!");
            return 0;
        }

        public void PrintArticles()
        {
            int i;
            var message = new MessageToUser();

            for (i = 0; i < maxInventoryRow; i++)
            {
                // End loop if no more articles
                if (String.IsNullOrEmpty(inventoryTable[i, 0]))
                {
                    // If there was something to print, then wait for user input before break
                    if (i > 0)
                    {
                        message.Write("End of list!");
                    }
                    break;
                }
                
                // Print the article info
                if (i == 0)
                {
                    // Write header before printing first article
                    Console.WriteLine("{0,-30} {1,-10} {2,-10}\n", "Article","Price", "Quantity");
                }
                Console.WriteLine("{0,-30} {1,-10} {2,-10}", inventoryTable[i, 0], inventoryTable[i, 1], inventoryTable[i, 2]);
            }
            return;
        }
        
        public void CreateArticles()
        {
            inventoryTable[0, 0] = "Coca Cola"; inventoryTable[0, 1] = "12"; inventoryTable[0, 2] = "100";
            inventoryTable[1, 0] = "Fanta"; inventoryTable[1, 1] = "11"; inventoryTable[1, 2] = "110";
            inventoryTable[2, 0] = "Sprite"; inventoryTable[2, 1] = "13"; inventoryTable[2, 2] = "120";
            inventoryTable[3, 0] = "Apelsin"; inventoryTable[3, 1] = "14"; inventoryTable[3, 2] = "130";
            inventoryTable[4, 0] = "Päron"; inventoryTable[4, 1] = "15"; inventoryTable[4, 2] = "140";
            inventoryTable[5, 0] = "Äpple"; inventoryTable[5, 1] = "16"; inventoryTable[5, 2] = "150";
            inventoryTable[6, 0] = "Mandarin"; inventoryTable[6, 1] = "17"; inventoryTable[6, 2] = "160";
            inventoryTable[7, 0] = "Mango"; inventoryTable[7, 1] = "18"; inventoryTable[7, 2] = "2";
            inventoryTable[8, 0] = "Ananas"; inventoryTable[8, 1] = "19"; inventoryTable[8, 2] = "33";
            inventoryTable[9, 0] = "Kiwi"; inventoryTable[9, 1] = "20"; inventoryTable[9, 2] = "14";
            return;
        }
    }
}