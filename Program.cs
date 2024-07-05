using ACCOUNTS;
using System;
using System.Globalization;

// INJECT ACCOUNTS LIST
Account[] objAccount = new Account[1];
using (StreamReader reader = new StreamReader("ACCOUNT_LIST.txt"))
{
    int size = Convert.ToInt32(Decrypt(reader.ReadLine()!));
    objAccount = new Account[size];
    for (int index = 0; index < objAccount.Length; index++)
    {
        objAccount[index] = new Account(Decrypt(reader.ReadLine()!), Convert.ToInt32(Decrypt(reader.ReadLine()!)), Decrypt(reader.ReadLine()!), Decrypt(reader.ReadLine()!), Convert.ToDouble(Decrypt(reader.ReadLine()!)), Decrypt(reader.ReadLine()!));
    }
}

string  adminLogin;
int adminPin;

using (StreamReader reader = new StreamReader("ADMIN_LOGIN.txt"))
{
    adminLogin = Decrypt(reader.ReadLine()!);
    adminPin = Convert.ToInt32(Decrypt(reader.ReadLine()!));
}

// PROGRAM START
Console.ResetColor();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("*** CLOCKWORK ATM ***");
Console.ResetColor();

// Launch appropriate menu if login & PIN match. 3 tries.

for (int i = 1; i <= 3; i++)
{
    Console.WriteLine("\nPlease log in to your account");
    Console.Write("Login: ");
    Console.ForegroundColor = ConsoleColor.Green;
    string login = Console.ReadLine();
    Console.ResetColor();
    Console.Write("PIN code: ");
    Console.ForegroundColor = ConsoleColor.Green;
    var pin = Convert.ToInt32(Console.ReadLine());
    Console.ResetColor();

    for (int index = 0; index < objAccount.Length; index++)
    {

        if (login == objAccount[index].accLogin && pin == objAccount[index].accPin)
        {
            CustomerMenu(index, objAccount);
            break;
        }
        else if (login == adminLogin && pin == adminPin)
        {
            AdminMenu(objAccount);
            break;
        }
        
    }
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Login or PIN incorrect. Please, try again. ({i}/3)");
    Console.ResetColor();
}

//  [CUSTOMER MENU]
static void CustomerMenu(int customerIndex, Account[] objAccount)
{
    Console.Clear();
    int CustomerMenuChoice = 0;
    do
    {
        Console.Clear();
        Console.ResetColor();
        Console.WriteLine("------------------------ CUSTOMER MENU ------------------------");
        Console.WriteLine("\n1----Withdraw Cash\n2----Cash Transfer\n3----Deposit Cash\n4----Display Balance\n5----Exit");
        Console.Write("Please select one of the above options: ");
        Console.ForegroundColor= ConsoleColor.Green;

        // try/catch changing the answer to int
        try
        {
            CustomerMenuChoice = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
            if (CustomerMenuChoice == 1 || CustomerMenuChoice == 2 || CustomerMenuChoice == 3 || CustomerMenuChoice == 4 || CustomerMenuChoice == 5)
            {
                switch (CustomerMenuChoice)
                {
                    case 1:
                        WithdrawCashMenu(customerIndex, objAccount);
                        break;
                    case 2:
                        CashTransfer(customerIndex, objAccount);
                        break;
                    case 3:
                        DepositCash(customerIndex, objAccount);
                        break;
                    case 4:
                        DisplayBalance(customerIndex, objAccount);
                        break;
                    case 5:
                        // Save data before exit
                        using (StreamWriter writer = new StreamWriter("ACCOUNT_LIST.txt"))
                        {
                            writer.WriteLine(Encrypt(Convert.ToString(objAccount.Length)));
                            for (int index = 0; index < objAccount.Length; index++)
                            {
                                writer.WriteLine(Encrypt(objAccount[index].accLogin));
                                writer.WriteLine(Encrypt(Convert.ToString(objAccount[index].accPin)));
                                writer.WriteLine(Encrypt(objAccount[index].accName));
                                writer.WriteLine(Encrypt(objAccount[index].accType));
                                writer.WriteLine(Encrypt(Convert.ToString(objAccount[index].accBalance)));
                                writer.WriteLine(Encrypt(objAccount[index].accStatus));
                            }
                        }
                        Environment.Exit(1);
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Choose a valid option (1-5).");
            }
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please use only integers to choose one of the options.");
        }
    } while (CustomerMenuChoice != 5);
    Console.ResetColor();
   

}

// WITHDRAW CASH MENU
static void WithdrawCashMenu(int customerIndex, Account[] objAccount)
{
    Console.Clear();
    Console.ResetColor();
    var withdrawMenuChoice = 0;
    do
    {
        Console.ResetColor();
        Console.WriteLine("------------------------ WITHDRAW CASH ------------------------");
        Console.WriteLine("\n1----Fast Cash\n2----Normal Cash\n3----Back");
        Console.Write("Please select one of the above options: ");
        Console.ForegroundColor= ConsoleColor.Green;
        try
        {
            withdrawMenuChoice = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
            if (withdrawMenuChoice == 1 || withdrawMenuChoice == 2 || withdrawMenuChoice == 3)
            {
                switch (withdrawMenuChoice)
                {
                    case 1:
                        FastCash(customerIndex, objAccount);
                        break;
                    case 2:
                        NormalCash(customerIndex, objAccount);
                        break;
                    case 3:
                        CustomerMenu(customerIndex, objAccount);
                        break;
                }
                CustomerMenu(customerIndex, objAccount);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Choose a valid option (1-3).");
            }
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please use only integers to choose one of the options.");
        }
    } while (withdrawMenuChoice != 5) ;


}

// FAST CASH
static void FastCash(int customerIndex, Account[] objAccount) 
{
    var fastCashChoice = 0;
    do
    {
        Console.Clear();
        Console.ResetColor();
        Console.WriteLine("------------------------ FAST CASH ------------------------");
        Console.WriteLine("\n1----50\n2----100\n3----200\n4----500\n5----1000\n6----1500\n7----2000\n8----Back");
        Console.Write("Please select one of the above options: ");
        Console.ForegroundColor= ConsoleColor.Green;
        try
        {
            fastCashChoice = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
            if (fastCashChoice == 1 || fastCashChoice == 2 || fastCashChoice == 3 || fastCashChoice == 4 || fastCashChoice == 5 || fastCashChoice == 6 || fastCashChoice == 7 || fastCashChoice == 8)
            {
                switch (fastCashChoice)
                {
                    case 1:
                        Withdraw(50, customerIndex, objAccount);
                        break;
                    case 2:
                        Withdraw(100, customerIndex, objAccount);
                        break;
                    case 3:
                        Withdraw(200, customerIndex, objAccount);
                        break;
                    case 4:
                        Withdraw(500, customerIndex, objAccount);
                        break;
                    case 5:
                        Withdraw(1000, customerIndex, objAccount);
                        break;
                    case 6:
                        Withdraw(1500, customerIndex, objAccount);
                        break;
                    case 7:
                        Withdraw(2000, customerIndex, objAccount);
                        break;
                    case 8:
                        CustomerMenu(customerIndex,objAccount);
                        break;
                }
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Choose a valid option (1-8).");
            }
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please use only integers to choose one of the options.");
        }

    } while(fastCashChoice != 8);
}

// NORMAL CASH
static void NormalCash(int accountIndex, Account[] objAccount)
{
    Console.Clear();
    double amount = -1;
    do
    {
        Console.ResetColor();
        Console.WriteLine("------------------------ NORMAL CASH ------------------------");
        Console.WriteLine("\nCLOCKWORK ATM Can withdraw any amount that is a multiple of 10");
        Console.Write("\nPlease type the amount you wish to withdraw (0 to go back): ");
        Console.ForegroundColor= ConsoleColor.Green;
        try
        {
            amount  = Convert.ToDouble(Console.ReadLine());
            if (amount > 0 && (amount % 10 == 0))
            {
                Withdraw(amount, accountIndex, objAccount);
                break;
            } else if (amount == 0)
            {
                CustomerMenu(accountIndex, objAccount);
            }
            else
            {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("\nPlease input a correct amount (multiple of 10) or 0 to go back\n");
                amount = -1;
            }
        }
        catch
        {
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("\nPlease input a correct amount (multiple of 10) or 0 to go back\n");
        }

    }while(amount == -1);
    CustomerMenu(accountIndex, objAccount);
}

// WITHDRAW
static void Withdraw(double amount, int accountIndex, Account[] objAccount)
{
    double withdrawLimit = 2000;
    double withdrawnAmount = 0;
    string withdrawConfirmation = null;
    bool currentDate = true;
    using (StreamReader reader = new StreamReader("WITHDRAW.txt"))
    {
        if (Decrypt(reader.ReadLine()!) == DateOnly.FromDateTime(DateTime.Now).ToString())
        {
            string line = reader.ReadLine();
            while (line != null) 
            {
                if (Decrypt(line).Contains("Index"))
                {
                    line = Decrypt(reader.ReadLine()!);
                    if (Convert.ToInt32(line) == accountIndex)
                    {
                        line = Decrypt(reader.ReadLine()!);
                        withdrawnAmount = Convert.ToDouble(line);
                        break;
                    }
                    
                }
                line = reader.ReadLine();
            }
        }
        else 
        {
            currentDate = false;
        }
    }
    if (!currentDate)
    {
        File.WriteAllText("WITHDRAW.txt", Encrypt(DateOnly.FromDateTime(DateTime.Now).ToString()));
        currentDate = true;
    }

    if (withdrawnAmount + amount < withdrawLimit)
    {
        do
        {
            Console.ResetColor();
            Console.Write($"Are you sure you want to withdraw {amount} PLN (Y/N)? ");
            Console.ForegroundColor = ConsoleColor.Green;
            withdrawConfirmation = Console.ReadLine();
            if (withdrawConfirmation == "Y" || withdrawConfirmation == "y")
            {
                if (amount <= objAccount[accountIndex].accBalance)
                {
                    Console.Clear();
                    Console.ResetColor();
                    objAccount[accountIndex].accBalance -= amount;
                    Console.WriteLine("Cash Successfully Withdrawn!");

                    // Update WITHDRAW.txt
                    List<string> lines = new List<string>(File.ReadAllLines("WITHDRAW.txt"));
                    bool found = false;
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (Decrypt(lines[i]).Contains("Index") && Decrypt(lines[i+1]) == accountIndex.ToString())
                        {
                            var current = Convert.ToInt32(Decrypt(lines[i + 2]));
                            lines[i + 2] = Encrypt(Convert.ToString(current + amount));
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        lines.Add(Encrypt("Index"));
                        lines.Add(Encrypt($"{accountIndex}"));
                        lines.Add(Encrypt($"{amount}"));
                    }
                    File.WriteAllLines("WITHDRAW.txt", lines);
                    using (StreamWriter writer = new StreamWriter("RECORDS.txt", append: true))
                    {
                        writer.WriteLine(Encrypt(Convert.ToString(DateOnly.FromDateTime(DateTime.Now))!));
                        writer.WriteLine(Encrypt("Withdrawal"));
                        writer.WriteLine(Encrypt($"{accountIndex}"));
                        writer.WriteLine(Encrypt($"{amount}"));
                        writer.WriteLine(Encrypt("N/A"));
                    }

                    string receipt = null;
                    do
                    {
                        Console.ResetColor();
                        Console.Write("\nDo you wish to print a receipt (Y/N)? ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        receipt = Console.ReadLine();
                        if (receipt == "Y" || receipt == "y")
                        {
                            Console.Clear();
                            Console.ResetColor();
                            Console.WriteLine("--- Receipt ---");
                            Console.WriteLine($"\nAccount #{accountIndex+1}");
                            Console.WriteLine($"\nDate: {DateTime.Now.ToString()}");
                            Console.WriteLine($"\nWithdrawn: {amount} PLN");
                            Console.WriteLine($"\nAccount Balance: {objAccount[accountIndex].accBalance}");
                            Console.WriteLine("\n--------------------");
                            Console.Write("\nPress any key to go back to Menu: ");
                            Console.ReadKey();
                            break;
                        }
                        else if (receipt == "N" || receipt == "n")
                        {
                            CustomerMenu(accountIndex, objAccount);
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please answer only Y/N");
                            receipt = null;
                        }
                    } while (receipt == null);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Insufficient funds.");
                    Console.WriteLine("Press any key to go back to previous menu");
                    Console.ReadKey();
                    FastCash(accountIndex, objAccount);
                    break;
                }
            }
            else if (withdrawConfirmation == "N" || withdrawConfirmation == "n")
            {
                FastCash(accountIndex, objAccount);
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please answer only Y/N");
                withdrawConfirmation = null;
            }
        } while (withdrawConfirmation == null);

    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nSorry, you have reached your withdrawal limit for today.");
        Console.ResetColor();
        Console.Write("\nPress any key to go back to Menu: ");
        Console.ReadKey();
    }
}

// CASH TRANSFER
static void CashTransfer(int accountIndex, Account[] objAccount)
{
    Console.Clear();
    Console.ResetColor();
    double amount = -1;
    Console.WriteLine("------------------------ CASH TRANSFER ------------------------");
    do
    {
        Console.Write("\nEnter the amount you wish to transfer (or type 0 to go back): ");
        Console.ForegroundColor = ConsoleColor.Green;
        try
        {
            amount = Convert.ToDouble(Console.ReadLine());
            if (amount > 0 && amount < objAccount[accountIndex].accBalance)
            {
                int targetAccount = -1;
                do
                {
                    Console.ResetColor();
                    Console.Write("\nPlease enter the account number you wish to transfer money to: ");
                    Console.ForegroundColor=ConsoleColor.Green;
                    try
                    {
                        targetAccount = Convert.ToInt32(Console.ReadLine());
                        if (targetAccount > 0 && targetAccount < objAccount.Length && targetAccount-1 != accountIndex)
                        {
                            int targetAccountConfirmation = -1;
                            
                                do
                                {
                                    for (int i = 0; i <= 3; i++)
                                    {
                                        Console.ResetColor();
                                        Console.WriteLine($"\nYou wish to transfer {amount} PLN to account #{targetAccount} held by {objAccount[targetAccount-1].accName}.");
                                        Console.Write("If this information is correct, please re-enter the account number (or type 0 to go back): ");
                                        Console.ForegroundColor= ConsoleColor.Green;
                                        try
                                        {
                                            targetAccountConfirmation = Convert.ToInt32(Console.ReadLine());
                                            if (targetAccountConfirmation == targetAccount)
                                            {
                                                objAccount[accountIndex].accBalance -= amount;
                                                objAccount[targetAccount-1].accBalance += amount;
                                            using (StreamWriter writer = new StreamWriter("RECORDS.txt", append: true))
                                            {
                                                writer.WriteLine(Encrypt(Convert.ToString(DateOnly.FromDateTime(DateTime.Now))!));
                                                writer.WriteLine(Encrypt("Cash Transfer"));
                                                writer.WriteLine(Encrypt($"{accountIndex}"));
                                                writer.WriteLine(Encrypt($"{amount}"));
                                                writer.WriteLine(Encrypt($"{targetAccount}"));
                                            }
                                            Console.ResetColor();
                                                Console.WriteLine($"\n{amount} PLN transfered successfully to account #{targetAccount}");
                                            Console.WriteLine("\nPress any key to go back to menu...");
                                            Console.ReadKey();
                                            CustomerMenu(accountIndex, objAccount);
                                            break;
                                            }
                                            else if (targetAccountConfirmation == 0)
                                            {
                                                CashTransfer(accountIndex, objAccount);
                                            }
                                            else 
                                            {

                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine($"Please re-enter correct account number or type 0 to go back. ({i}/3)");
                                            }
                                        }
                                        catch
                                        {
                                            Console.ForegroundColor=ConsoleColor.Red;
                                            Console.WriteLine("Please use only numbers.");
                                        }
                                    }
                                } while (targetAccountConfirmation == -1);
                            
                        }
                        else if (targetAccount-1 == accountIndex)
                        {
                            Console.ForegroundColor=ConsoleColor.Red;
                            Console.WriteLine("\nYou cannot transfer money to your own account. Please choose a different account.");
                            targetAccount = -1;
                        }
                        else
                        {
                            Console.ForegroundColor=ConsoleColor.Red;
                            Console.WriteLine("\nChoose a valid account number.");
                            targetAccount = -1;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor=ConsoleColor.Red;
                        Console.WriteLine("Please use only numbers.");
                    }
                } while (targetAccount == -1);
            }
            else if (amount == 0)
            {
                CustomerMenu(accountIndex, objAccount);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The amount must be greater than 0.");
                amount = -1;
            }
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please use only numbers");
        }


    }while (amount == -1);
}

// DEPOSIT CASH
static void DepositCash(int accountIndex, Account[] objAccount)
{
    Console.Clear();
    Console.ResetColor();
    double amount = -1;
    Console.WriteLine("------------------------ DEPOSIT CASH ------------------------");
    do
    {
        Console.Write("\nPlease enter the amount you wish to deposit (or type 0 to go back): ");
        Console.ForegroundColor = ConsoleColor.Green;
        try
        {
            amount = Convert.ToDouble(Console.ReadLine());
            if (amount > 0)
            {
                Console.ResetColor();
                objAccount[accountIndex].accBalance += amount;
                Console.WriteLine($"\n{amount} PLN deposited successfully.");
                using (StreamWriter writer = new StreamWriter("RECORDS.txt", append: true))
                {
                    writer.WriteLine(Encrypt(Convert.ToString(DateOnly.FromDateTime(DateTime.Now))!));
                    writer.WriteLine(Encrypt("Deposit"));
                    writer.WriteLine(Encrypt($"{accountIndex}"));
                    writer.WriteLine(Encrypt($"{amount}"));
                    writer.WriteLine(Encrypt("N/A"));
                }
                string receipt = null;
                do
                {
                    Console.ResetColor();
                    Console.Write("\nDo you wish to print a receipt (Y/N)? ");
                    Console.ForegroundColor= ConsoleColor.Green;
                    receipt = Console.ReadLine();
                    if (receipt == "Y" || receipt == "y")
                    {
                        Console.Clear();
                        Console.ResetColor();
                        Console.WriteLine("--- Receipt ---");
                        Console.WriteLine($"\nAccount #{accountIndex+1}");
                        Console.WriteLine($"\nDate: {DateTime.Now.ToString()}");
                        Console.WriteLine($"\nDeposited: {amount} PLN");
                        Console.WriteLine($"\nAccount Balance: {(objAccount[accountIndex].accBalance)}");
                        Console.WriteLine("\n--------------------");
                        Console.Write("\nPress any key to go back to Menu: ");
                        Console.ReadKey();
                        break;
                    }
                    else if (receipt == "N" || receipt == "n")
                    {
                        CustomerMenu(accountIndex, objAccount);
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("Please answer only Y/N");
                        receipt = null;
                    }
                } while (receipt == null);
            }
            else if (amount == 0)
            {
                CustomerMenu(accountIndex, objAccount);
            }
            else
            {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("Please enter a valid amount (or type 0 to go back).");
                amount= -1;
            }
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please use only numbers");
        }
    } while (amount == -1);
}

// DISPLAY BALANCE
static void DisplayBalance(int accountIndex, Account[] objAccount)
{
    Console.Clear();
    Console.ResetColor();
    Console.WriteLine("------------------------ ACCOUNT BALANCE ------------------------");
    Console.WriteLine($"\nYour account's balance is: {objAccount[accountIndex].accBalance}");
    Console.WriteLine("\nPress any key to go back...");
    Console.ReadKey();
    CustomerMenu(accountIndex, objAccount);
}


// ----------------- ADMIN PART ---------------------

// [ADMIN MENU]s
static void AdminMenu(Account[] objAccount)
{
    Console.Clear();
    int AdminMenuChoice = 0;
    do
    {
        Console.ResetColor();
        Console.WriteLine("------------------------ ADMIN MENU ------------------------");
        Console.WriteLine("\n1----Create New Account. \n2----Delete Existing Account. \n3----Update Account Information.  \n4----Search for Account. \n5----View Reports \n6----Exit");
        Console.Write("Please select one of the above options: ");
        Console.ForegroundColor= ConsoleColor.Green;

        // try/catch changing the answer to int
        try                                                                
        {
            AdminMenuChoice = Convert.ToInt32(Console.ReadLine());
            if (AdminMenuChoice >= 1 && AdminMenuChoice <= 6)
            {
                Console.ResetColor();
                switch (AdminMenuChoice)
                {
                    case 1:
                        CreateNew(objAccount);
                        break;
                    case 2:
                        DeleteExisting(objAccount);
                        break;
                    case 3:
                        UpdateInfo(objAccount);
                        break;
                    case 4:
                        SearchAcc(objAccount);
                        break;
                    case 5:
                        ViewReports(objAccount);
                        break;
                    case 6:
                        string[] empty = new string[0];
                        File.WriteAllLines("ACCOUNT_LIST.txt", empty);
                        using (StreamWriter writer = new StreamWriter("ACCOUNT_LIST.txt"))
                        {
                            writer.WriteLine(Encrypt(Convert.ToString(objAccount.Length)));
                            for (int index = 0; index < objAccount.Length; index++)
                            {
                                writer.WriteLine(Encrypt(objAccount[index].accLogin));
                                writer.WriteLine(Encrypt(Convert.ToString(objAccount[index].accPin)));
                                writer.WriteLine(Encrypt(objAccount[index].accName));
                                writer.WriteLine(Encrypt(objAccount[index].accType));
                                writer.WriteLine(Encrypt(Convert.ToString(objAccount[index].accBalance)));
                                writer.WriteLine(Encrypt(objAccount[index].accStatus));
                            }
                        }
                        Environment.Exit(1);
                        break;

                }
            } else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Choose a valid option (1-6).");
            }
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please use only integers to choose one of the options.");
        }
    } while (AdminMenuChoice != 6);
    Console.ResetColor();
}

// [ADMIN MENU] - Creating
static void CreateNew(Account[] objAccount)
{
    Console.Clear();
    Console.ResetColor();
    Console.WriteLine("Please enter data for the new account:");
    string newLogin = null;
    do
    {
        Console.ResetColor();
        Console.Write("Login: ");
        Console.ForegroundColor=ConsoleColor.Green;
        string tempLogin = Console.ReadLine();
        if (tempLogin != null)
        {
            newLogin = tempLogin;
        }
        else
        {
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("Please enter login.");
        }
    } while (newLogin == null);

    // do-while loop making sure PIN is digits only
    int newPin = -1;
    do
    {
        Console.ResetColor();
        Console.Write("PIN: ");
        Console.ForegroundColor=ConsoleColor.Green;
        try
        {
            newPin = Convert.ToInt32(Console.ReadLine());
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("PIN invalid. Please use only integers.");
        }
    } while (newPin == -1);

    string newName = null;

    do
    {
        Console.ResetColor();
        Console.Write("Holder's Name: ");
        Console.ForegroundColor=ConsoleColor.Green;
        string tempName = Console.ReadLine();
        if (tempName != null)
        {
            newName = tempName;
        }
        else
        {
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("Please enter a name.");
        }
    } while (newName == null);


    // do-while loop checking if newType is Savings/Current
    string newType = null;
    do
    {
        Console.ResetColor();
        Console.Write("Type (Savings/Current): ");
        Console.ForegroundColor=ConsoleColor.Green;
        string tempType = Console.ReadLine();

        if (tempType == "Savings" || tempType == "Current")
        {
            newType = tempType;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter a valid type (Savings/Current).");
        }
    } while (newType == null);


    // do-while loop checking if newBalance is digits only
    double newBalance = -1;
    do
    {
        Console.ResetColor();
        Console.Write("Starting Balance: ");
        Console.ForegroundColor=ConsoleColor.Green;
        try
        {
            newBalance = Convert.ToDouble(Console.ReadLine());
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Balance invalid. Please use only numbers.");
        }
    } while (newBalance == -1);

    // do-while loop checing if newStatus is Active/Disabled
    string newStatus = null;
    do
    {
        Console.ResetColor();
        Console.Write("Status (Active/Disabled): ");
        Console.ForegroundColor=ConsoleColor.Green;
        string tempStatus = Console.ReadLine();
        if (tempStatus == "Active" || tempStatus == "Disabled")
        {
            newStatus = tempStatus;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter a valid status (Active/Disabled).");
        }
    } while (newStatus == null);

    Console.ResetColor();
    List<Account> AccountList = new List<Account>();
    AccountList = objAccount.ToList();
    AccountList.Add(new Account(newLogin, newPin, newName, newType, newBalance, newStatus));
    objAccount = AccountList.ToArray();
    Console.WriteLine($"Account Successfully Created – the account number assigned is: {objAccount[objAccount.Length-1].accNum}");
    Console.WriteLine("Press any key to go back to menu...");
    Console.ReadKey();
    AdminMenu(objAccount);
}

// [ADMIN MENU] - Deleting
static void DeleteExisting(Account[] objAccount)
{
    Console.Clear();
    int accChoice = -1;
    do
    {
        Console.ResetColor();
        Console.Write("Enter the account number to which you want to delete: ");
        Console.ForegroundColor=ConsoleColor.Green;
        try
        {
            accChoice = Convert.ToInt32(Console.ReadLine());
            if (accChoice > 0 && accChoice <= objAccount.Length)
            {
                accChoice -= 1;             
                int accConfirmation = -1;
                do
                {
                    Console.ResetColor();
                    Console.Write($"\nYou wish to delete the account held by {objAccount[accChoice].accName}.\nIf this information is correct please re-enter the account number or type '0' to go back: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    try
                    {
                        accConfirmation = Convert.ToInt32(Console.ReadLine());
                        if (accChoice == accConfirmation -1)
                        {
                            Console.ResetColor();
                            List<Account> AccountList = new List<Account>();
                            AccountList = objAccount.ToList();
                            AccountList.RemoveAt(accChoice);
                            objAccount = AccountList.ToArray();

                            Console.WriteLine("\nAccount Deleted Successfully.");
                            Console.WriteLine("\nPress any key to go back to menu.");
                            Console.ReadKey();
                            AdminMenu(objAccount);

                        }
                        else if (accConfirmation == 0)
                        {
                            AdminMenu(objAccount);
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.WriteLine("\nThe account numbers do not match.");
                            string response = "again";
                            do
                            {
                                Console.Write("\nDo you wish to try again (Y/N)? ");
                                response = Console.ReadLine();
                                if (response == "Y" || response == "y")
                                {
                                    DeleteExisting(objAccount);
                                }
                                else if (response == "N" || response == "n")
                                {
                                    AdminMenu(objAccount);
                                }
                                else
                                {
                                    response = "again";
                                }
                            } while (response == "again");

                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease use only numbers.");
                    }
                } while (accConfirmation == -1);
            }
            else
            {
                Console.ResetColor();
                Console.WriteLine("\nThe number does not match any of the accounts.");
                string response = "again";
                do
                {
                    Console.Write("\nDo you wish to try again (Y/N)? ");
                    response = Console.ReadLine();
                    if (response == "Y" || response == "y")
                    {
                        DeleteExisting(objAccount);
                    }
                    else if (response == "N" || response == "n")
                    {
                        AdminMenu(objAccount);
                    }
                    else
                    {
                        response = "again";
                    }
                } while (response == "again");
            }
        }
        catch
        {
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("\nPlease use only numbers.");
        }
    } while (accChoice == -1);

}

// [ADMIN MENU] - Updating Info
static void UpdateInfo(Account[] objAccount)
{
    Console.Clear();
    int accChoice = -1;
    do
    {
        Console.ResetColor();
        Console.Write("Enter the Account Number: ");
        Console.ForegroundColor=ConsoleColor.Green;
        try
        {
            accChoice = Convert.ToInt32(Console.ReadLine());
            if (accChoice > 0 && accChoice <= objAccount.Length)
            {
                accChoice -= 1;
                string tempLogin = null;
                string tempName = null;
                string tempPin = null;
                string tempBalance = null;
                string tempType = null;
                string tempStatus = null;
                int newPin = -1;
                double newBalance = -1;
                Console.ResetColor();
                Console.WriteLine($"Account #{objAccount[accChoice].accNum}");
                Console.WriteLine($"Login: {objAccount[accChoice].accLogin}");
                Console.WriteLine($"Pin: {objAccount[accChoice].accPin}");
                Console.WriteLine($"Holder: {objAccount[accChoice].accName}");
                Console.WriteLine($"Balance: {objAccount[accChoice].accBalance}");
                Console.WriteLine($"Type: {objAccount[accChoice].accType}");
                Console.WriteLine($"Status: {objAccount[accChoice].accStatus}");
                Console.WriteLine("\nPlease enter in the fields you wish to update (leave blank otherwise):");
                Console.Write("Login: ");
                Console.ForegroundColor = ConsoleColor.Green;
                tempLogin = Console.ReadLine();
                if (tempLogin != "" && tempLogin != null)
                {
                    objAccount[accChoice].accLogin = tempLogin;
                }
                else
                {
                }

                do
                {
                    Console.ResetColor();
                    Console.Write("Pin: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    tempPin = Console.ReadLine();
                    if (tempPin != "" && tempPin != null)
                    {
                        try
                        {
                            newPin = Convert.ToInt32(tempPin);
                        }
                        catch
                        {
                            Console.ForegroundColor= ConsoleColor.Red;
                            Console.WriteLine("\nPlease use only numbers");
                            newPin = -1;
                        }
                        objAccount[accChoice].accPin = newPin;
                    }
                    else
                    {
                        newPin = 0;
                        
                    }
                } while (newPin == -1);

                Console.ResetColor();
                Console.Write("Holder's name: ");
                Console.ForegroundColor = ConsoleColor.Green;
                tempName = Console.ReadLine();
                if (tempName != "" && tempName != null)
                {
                    objAccount[accChoice].accName = tempName;
                }
                else
                {
                    
                }
                do
                {
                    Console.ResetColor();
                    Console.Write("Balance: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    tempBalance = Console.ReadLine();
                    if (tempBalance != "" && tempBalance != null)
                    {
                    
                        try
                        {
                            newBalance = Convert.ToDouble(tempBalance);
                            if (newBalance >= 0)
                            {
                                objAccount[accChoice].accBalance = newBalance;
                            }
                            else
                            {  
                                Console.ForegroundColor=ConsoleColor.Red;
                                Console.WriteLine("\nBalance must be greater or equal to 0.");
                                newBalance = -1;
                            }
                        }
                        catch
                        {
                            Console.ForegroundColor= ConsoleColor.Red;
                            Console.WriteLine("\nPlease use only numbers");
                            newBalance = -1;
                        }
                    }
                    else
                    {
                        newBalance = 0;
                        
                    }
                } while (newBalance == -1);

                do
                {
                    Console.ResetColor();
                    Console.Write("Type: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    tempType = Console.ReadLine();
                    if (tempType != "" && tempType != null)
                    {
                        if (tempType == "Savings" || tempType == "Current")
                        {

                            objAccount[accChoice].accType = tempType;
                        }
                        else
                        {
                            Console.ForegroundColor=ConsoleColor.Red;
                            Console.WriteLine("\nPlease input correct type (Savings/Current).");
                            tempType = "again";
                        }
                    }
                    else
                    {
                        
                    }
                } while (tempType == "again");

                do
                {
                    Console.ResetColor();
                    Console.Write("Status: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    tempStatus = Console.ReadLine();
                    if (tempStatus != "" && tempStatus != null)
                    {
                        if (tempStatus == "Active" || tempStatus == "Disabled")
                        {

                            objAccount[accChoice].accStatus = tempStatus;
                        }
                        else
                        {
                            Console.ForegroundColor=ConsoleColor.Red;
                            Console.WriteLine("\nPlease input correct status (Active/Disabled).");
                            tempStatus = "again";
                        }
                    }
                    else
                    {
                        
                    }
                } while (tempStatus == "again");
                    Console.ResetColor();
                    Console.WriteLine("\nAccount information successfully updated.");
                    Console.WriteLine("Press any key to go back to menu.");
                    Console.ReadKey();
                    AdminMenu(objAccount);

            }
            else
            {
                string response = "again";
                do
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The number does not match any of the accounts.");
                    Console.ResetColor();
                    Console.Write("Do you want to try again (Y/N)? ");
                    Console.ForegroundColor=ConsoleColor.Green;
                    response = Console.ReadLine();
                    if (response == "y" || response == "Y")
                    {
                        UpdateInfo(objAccount);
                    }
                    else if (response == "N" || response == "n")
                    {
                        AdminMenu(objAccount);
                    }
                    else
                    {
                        
                    }
                } while (response == "again");
            }
        }
        catch
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Please use only numbers.");
            accChoice = -1;
        }
    } while (accChoice == -1);
}

//[ADMIN MENU] - Search menu
static void SearchAcc(Account[] objAccount)
{
    string tempNum = null;
    string tempLogin = null;
    string tempName = null;
    string tempBalance = null;
    string tempType = null;
    string tempStatus = null;

    int accNum = -1;
    string accLogin = null;
    string accName = null;
    double accBalance = -1;
    string accType = null;
    string accStatus = null;

    Console.Clear();
    Console.ResetColor();

    Console.WriteLine("\nPlease enter the search parameters you want to use: ");
    do
    {
        Console.Write("\nAccount #: ");
        Console.ForegroundColor=ConsoleColor.Green;
        tempNum = Console.ReadLine();
        if (tempNum != null && tempNum != "")
        {
            try
            {
                accNum = Convert.ToInt32(tempNum);
                if (accNum >= 0)
                {

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Account # incorrect.");
                    tempNum = null;
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please use only numbers.");
            }
        }
        else
        {
            tempNum = "continue";
        }
    } while (tempNum == null);

    Console.ResetColor();
    Console.Write("Login: ");
    Console.ForegroundColor=ConsoleColor.Green;
    tempLogin = Console.ReadLine();
    if (tempLogin != null && tempLogin != "")
    {
        accLogin = tempLogin;
    }
    else
    {

    }

    Console.ResetColor();
    Console.Write("Holder's name: ");
    Console.ForegroundColor=ConsoleColor.Green;
    tempName = Console.ReadLine();
    if (tempName != null && tempName != "")
    {
        accName = tempName;
    }
    else
    {

    }

    do
    {
        Console.ResetColor();
        Console.Write("Account balance: ");
        Console.ForegroundColor=ConsoleColor.Green;
        tempBalance = Console.ReadLine();
        if (tempBalance != null && tempBalance != "")
        {
            try
            {
                accBalance = Convert.ToDouble(tempBalance);
                if (accBalance >= 0)
                {

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nAccount Balance incorrect.");
                    tempBalance = null;
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPlease use only numbers.");
            }
        }
        else
        {
            tempBalance = "continue";
        }
    } while (tempBalance == null);

    do 
    { 
        Console.ResetColor();
        Console.Write("Account type: ");
        Console.ForegroundColor=ConsoleColor.Green;
        tempType = Console.ReadLine();
        if (tempType != null && tempType != "")
        {
            if (tempType == "Savings" || tempType == "Current")
            {
                accType = tempType;

            }
            else
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("\n Account type can only be Savings or Current.");
                tempType = null;
            }
        }
        else
        {
            tempType ="continue";
        }
    }while (tempType == null) ;

    do
    {
        Console.ResetColor();
        Console.Write("Account status: ");
        Console.ForegroundColor=ConsoleColor.Green;
        tempStatus = Console.ReadLine();
        if (tempStatus != null && tempStatus != "")
        {
            if (tempStatus == "Active" || tempStatus == "Disable")
            {
                accStatus = tempStatus;

            }
            else
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("\n Account status can only be Active or Disabled.");
                tempStatus = null;
            }
        }
        else
        {
            tempStatus ="continue";
        }
    } while (tempStatus == null);

    Console.ResetColor();
    Console.WriteLine("\n==== SEARCH RESULTS ======");
    Console.WriteLine("\nAccount ID  Login       Holders Name    Balance     Type        Status");
    Console.WriteLine();
    for (int i = 0; i < objAccount.Length; i++)
    {
        if (objAccount[i].accNum == accNum || objAccount[i].accLogin == accLogin || objAccount[i].accName == accName || objAccount[i].accBalance == accBalance || objAccount[i].accType == accType || objAccount[i].accStatus == accStatus)
        {
            Console.WriteLine($"{objAccount[i].accNum,-10}  {Truncate(objAccount[i].accLogin, 10),-10}  {Truncate(objAccount[i].accName, 14),-14}  {objAccount[i].accBalance,10:C0}  {objAccount[i].accType,-10}  {objAccount[i].accStatus,-10}");
        }
    }

    Console.WriteLine("\n==========================");
    Console.WriteLine("Press any key to go back to menu...");
    Console.ReadKey();
    AdminMenu(objAccount);
}

// [ADMIN MENU] - View reports
static void ViewReports(Account[] objAccount)
{
    Console.Clear();
    Console.ResetColor();
    Console.WriteLine("\n1----Accounts by balance. \n2----Transactions by date. \n3----Back.");
    Console.Write("Please select one of the above options: ");
    Console.ForegroundColor = ConsoleColor.Green;
    try
    {
        int reportChoice = Convert.ToInt32(Console.ReadLine());
        if (reportChoice == 1)
        {
            double? min = null;
            double? max = null;
            Console.Clear();
            try
            {
                Console.ResetColor();
                Console.Write("Enter MINIMUM amount: ");
                Console.ForegroundColor = ConsoleColor.Green;
                min = Convert.ToDouble(Console.ReadLine());

            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please input a valid sum.");
            }
            try
            {
                Console.ResetColor();
                Console.Write("Enter MAXIMUM amount: ");
                Console.ForegroundColor = ConsoleColor.Green;
                max = Convert.ToDouble(Console.ReadLine());

            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please input a valid sum.");
            }

            Console.ResetColor();
            Console.WriteLine("==== REPORT ======");
            Console.WriteLine("Account ID  Login       Holders Name    Balance     Type        Status");
            for (int i = 0; i < objAccount.Length; i++)
            {
                if (objAccount[i].accBalance >= min && objAccount[i].accBalance <= max)
                {
                    Console.WriteLine($"{objAccount[i].accNum,-10}  {Truncate(objAccount[i].accLogin,10),-10}  {Truncate(objAccount[i].accName,14),-14}  {objAccount[i].accBalance,10:C0}  {objAccount[i].accType,-10}  {objAccount[i].accStatus,-10}");
                }
            }

        }
        else if (reportChoice == 2)
        {
            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MinValue;
            Console.Clear();
            try
            {
                Console.ResetColor();
                Console.Write("Enter the STARTING date (DD.MM.YYYY): ");
                Console.ForegroundColor = ConsoleColor.Green;
                DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out start);
                Console.ResetColor();
                Console.Write("Enter the ENDING date (DD.MM.YYYY): ");
                Console.ForegroundColor = ConsoleColor.Green;
                DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out end);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please use a correct format: DD.MM.YYYY");
            }
            Console.ResetColor();
            Console.WriteLine("==== REPORT ======");
            Console.WriteLine("Transaction    Account ID  Login       Holders Name    Amount      Recipient ID  Date");
            List<string> records = new List<string>(File.ReadAllLines("RECORDS.txt"));
            for (int i = 0; i < records.Count; i++)
            {
                DateTime current;
                if (DateTime.TryParseExact(Decrypt(records[i]), "dd.MM.yyyy", null, DateTimeStyles.None, out current))
                {
                    if(current >= start && current <= end)
                    {
                        var index = Convert.ToInt32(Decrypt(records[i+2]));
                        Console.WriteLine($"{Decrypt(records[i + 1]),-13}  {Decrypt(records[i+2])+1,-10}  {Truncate(objAccount[index].accLogin, 10),-10}  {Truncate(objAccount[index].accName, 14),-14}  {Convert.ToDouble(Decrypt(records[i+3])),10:C0}  {Decrypt(records[i+4]),-12}  {DateOnly.FromDateTime(current),-10}");
                    }
                }

            }

        }
        else if (reportChoice == 3)
        {
            AdminMenu(objAccount);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Choose a valid option (1-3).");
        }
        Console.ResetColor();
        Console.WriteLine("==================");
        Console.WriteLine("\nPress any key to go back to menu.");
        Console.ReadKey();
        AdminMenu(objAccount);

    }
    catch
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Please use only integers to choose one of the options.");
    }
}

// Additional Methods
static string Truncate(string text, int maxLength)
{
    if (string.IsNullOrEmpty(text))
        return text;
    return text.Length <= maxLength ? text : text.Substring(0, maxLength-3)+"...";
}
static string Encrypt(string text)
{
    char[] chars = new char[text.Length];
    char current;
    for (int i = 0; i < text.Length; i++)
    {
        if (Char.IsLetter(text[i]))
        {
            if (Char.IsUpper(text[i]))
            {
                current = (char)(((text[i] - 'A' + 19) % 26) + 'A');
            }
            else
            {
                current = (char)(((text[i] - 'a' + 19) % 26) + 'a');
            }
        }
        else if (Char.IsDigit(text[i]))
        {
            current = (char)(((text[i] - '0' + 6) % 10) + '0');
        }
        else
        {
            current = text[i];
        }
        chars[i] = current;
    }
    return new string(chars);
}

static string Decrypt(string text)
{
    char[] chars = new char[text.Length];
    char current;
    for (int i = 0; i < text.Length; i++)
    {
        if (Char.IsLetter(text[i]))
        {
            if (Char.IsUpper(text[i]))
            {
                current = (char)(((text[i] - 'A' - 19 + 26) % 26) + 'A');
            }
            else
            {
                current = (char)(((text[i] - 'a' - 19 + 26) % 26) + 'a');
            }
        }
        else if (Char.IsDigit(text[i]))
        {
            current = (char)(((text[i] - '0' - 6 + 10) % 10) + '0');
        }
        else
        {
            current = text[i];
        }
        chars[i] = current;
    }
    return new string(chars);
}

Console.ReadLine();