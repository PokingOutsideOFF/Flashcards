﻿// See https://aka.ms/new-console-template for more information
using Flashcards.Views;

public class Program
{
    static void Main()
    {
        var mainMenu = new MainMenu();
        mainMenu.Menu();
    }
}



// Add a readme to provide sql script to create the database

/*## Setup Instructions

1.Set the following environment variables on your local machine:
   - `DB_SERVER`: Your SQL Server instance name
   - `DB_NAME`: Your database name

2. Modify the configuration file to use these environment variables:
   ```xml
<configuration>
       < appSettings >
           < add key = "FlashcardsDBConnection" value = "Data Source=${DB_SERVER};Initial Catalog=${DB_NAME};Integrated Security=True;" />
       </ appSettings >
   </ configuration >*/