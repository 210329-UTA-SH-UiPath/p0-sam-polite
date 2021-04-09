using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Client.Singletons;

namespace PizzaBox.Client
{
  /// <summary>
  /// 
  /// </summary>
  internal class Program
  {
    private static readonly StoreSingleton _storeSingleton = StoreSingleton.Instance;
    private static readonly PizzaSingleton _pizzaSingleton = PizzaSingleton.Instance;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
      Run();
    }

    /// <summary>
    /// 
    /// </summary>
    private static void Run()
    {
      var order = new Order();

      Console.WriteLine("Welcome to PizzaBox");
      DisplayStoreMenu();

      order.Customer = new Customer();
      order.Store = SelectStore();
      order.Pizza = SelectPizza();
      var crust = listCrusts();
      var crustPrice = getCrustPrice(crust);
      var size = listSizes();
      var sizePrice = getSizePrice(size);
      var Toppings = getNumberToppings();
      List<int> ToppingChoices = getToppingChoice(Toppings);
      var ToppingsPrice = getToppingPrice(ToppingChoices);

      //list of toppings

      order.Save();
    }

    /// <summary>
    /// 
    /// </summary>
    private static void DisplayOrder(APizza pizza)
    {
      Console.WriteLine($"Your order is: {pizza}");
    }

    /// <summary>
    /// 
    /// </summary>
    private static void DisplayPizzaMenu()
    {
      var index = 0;

      foreach (var item in _pizzaSingleton.Pizzas)
      {
        Console.WriteLine($"{++index} - {item}");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    private static void DisplayStoreMenu()
    {
      var index = 0;

      foreach (var item in _storeSingleton.Stores)
      {
        Console.WriteLine($"{++index} - {item}");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static APizza SelectPizza()
    {
      var input = int.Parse(Console.ReadLine());
      var pizza = _pizzaSingleton.Pizzas[input - 1];

      DisplayOrder(pizza);

      return pizza;
    }

    /// <summary>
    /// after you select a store then a menu is displayed
    /// </summary>
    /// <returns></returns>
    private static AStore SelectStore()
    {
      var input = int.Parse(Console.ReadLine()); // be careful (think execpetion/error handling)

      DisplayPizzaMenu();

      return _storeSingleton.Stores[input - 1];
    }

    public static string listCrusts()
    {
      System.Console.WriteLine("Please choose a crust");
      int choiceNum = 0; // list the corresponding number to the crust choices in list
      string[] list = { "soft", "cheesy", "handtossed", "deepdish" };
      foreach (var crust in list)
      {
        System.Console.WriteLine(choiceNum + " - " + crust);
        choiceNum++;
      }

      string crustChoice = " ";
      int crustSwitch = int.Parse(Console.ReadLine());
      switch (crustSwitch)
      {
        case 0:
          crustChoice = list[0];
          System.Console.WriteLine("crust is " + list[0]);
          break;

        case 1:
          crustChoice = list[1];
          System.Console.WriteLine("crust is " + list[1]);
          break;

        case 2:
          crustChoice = list[2];
          System.Console.WriteLine("crust is " + list[2]);
          break;

        case 3:
          crustChoice = list[3];
          System.Console.WriteLine("crust is " + list[3]);
          break;

        default:
          System.Console.WriteLine("Crust number was not selected. Please choose a crust : 0 soft, 1 cheesy, 2 handtossed, 3 deepdish");
          listCrusts();
          break;
      }

      return crustChoice;

    }

    public static double getCrustPrice(string crustChoice)
    {
      double price = 0.0;
      if (crustChoice == "soft")
      {
        price = 1.50;
        System.Console.WriteLine($"soft ${price}");
      }
      else if (crustChoice == "cheesy")
      {
        price = 3.00;
        System.Console.WriteLine($"cheesy ${price}");
      }
      else if (crustChoice == "handtossed")
      {
        price = 4.00;
        System.Console.WriteLine($"handtossed ${price}");
      }
      else
      {
        price = 5.50;
        System.Console.WriteLine($"deepdish ${price}");
      }
      return price;
    }

    public static string listSizes()
    {
      System.Console.WriteLine("Please choose a size");
      int choiceNum = 0; // list the corresponding number to the size choices in list
      string[] list = { "small-10-inch", "medium-12-inch", "large-14-inch", "extra-large-16-inch" };
      foreach (var size in list)
      {
        System.Console.WriteLine(choiceNum + " - " + size);
        choiceNum++;
      }

      string sizeChoice = " ";
      int sizeSwitch = int.Parse(Console.ReadLine());
      switch (sizeSwitch)
      {
        case 0:
          sizeChoice = list[0];
          System.Console.WriteLine("size is " + list[0]);
          break;

        case 1:
          sizeChoice = list[1];
          System.Console.WriteLine("size is " + list[1]);
          break;

        case 2:
          sizeChoice = list[2];
          System.Console.WriteLine("size is " + list[2]);
          break;

        case 3:
          sizeChoice = list[3];
          System.Console.WriteLine("size is " + list[3]);
          break;

        default:
          System.Console.WriteLine("size number was not selected. Please choose a size : 0 small-10-inch  1 medium-12-inch  2 large-14-inch  3 extra-large-16-inch");
          listCrusts();
          break;
      }

      return sizeChoice;

    }


    public static double getSizePrice(string sizeChoice)
    {
      double price = 0.0;
      if (sizeChoice == "small-10-inch")
      {
        price = 5.50;
        System.Console.WriteLine($"small-10-inch ${price}");
      }
      else if (sizeChoice == "medium-12-inch")
      {
        price = 7.00;
        System.Console.WriteLine($"medium-12-inch ${price}");
      }
      else if (sizeChoice == "large-14-inch")
      {
        price = 8.00;
        System.Console.WriteLine($"large-14-inch ${price}");
      }
      else
      {
        price = 9.50;
        System.Console.WriteLine($"extra-large-16-inch ${price}");
      }
      return price;
    }

    public static int getNumberToppings()
    {

      int toppingTypeNum = 0;
      int toppingAdds = 0;
      System.Console.WriteLine("Enter how many toppings you would like. You can have up to five. Enter 0 for no toppings, 1 for one topping, 2 for two, 3 for three, 4 for four or 5 for five.");
      toppingTypeNum = int.Parse(Console.ReadLine());
      if (toppingTypeNum == 0)
      {
        toppingAdds = 0;
        System.Console.WriteLine("No toppings added.");
      }
      else if (toppingTypeNum == 1)
      {
        toppingAdds = 1;
      }
      else if (toppingTypeNum == 2)
      {
        toppingAdds = 2;
      }
      else if (toppingTypeNum == 3)
      {
        toppingAdds = 3;
      }
      else if (toppingTypeNum == 4)
      {
        toppingAdds = 4;
      }
      else if (toppingTypeNum == 5)
      {
        toppingAdds = 5;
      }
      else
      {
        System.Console.WriteLine("You did not enter a number for the amount of toppings.");
        getNumberToppings();
      }

      return toppingAdds;

    }
    //change to a list 
    public static List<int> getToppingChoice(int toppingAdds)
    {
      List<int> toppingList = new List<int>();
      int toppingType1 = 0;
      int toppingType2 = 0;
      int toppingType3 = 0;
      int toppingType4 = 0;
      int toppingType5 = 0;
      // one topping
      if (toppingAdds == 1)
      {
        System.Console.WriteLine("Enter the type of topping you would like: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType1 = int.Parse(Console.ReadLine());
        if (toppingType1 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType1 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType1 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType1 == 4)
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType1 == 5)
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the topping choice.");
          getToppingChoice(toppingAdds);
        }
        return toppingList;
      }
      //two toppings
      if (toppingAdds == 2)
      {
        System.Console.WriteLine("Enter the type of topping you would like for the first topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType1 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the second topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType2 = int.Parse(Console.ReadLine());
        if (toppingType1 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType1 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType1 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType1 == 4)
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType1 == 5)
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the first topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check second topping
        if (toppingType2 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType2 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType2 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType2 == 4)
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType2 == 5)
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the second topping choice.");
          getToppingChoice(toppingAdds);
        }
        return toppingList;
      }
      //three toppings
      if (toppingAdds == 3)
      {
        System.Console.WriteLine("Enter the type of topping you would like for the first topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType1 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the second topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType2 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the third topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType3 = int.Parse(Console.ReadLine());
        if (toppingType1 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType1 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType1 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType1 == 4)
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType1 == 5)
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the first topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check second topping
        if (toppingType2 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType2 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType2 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType2 == 4)
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType2 == 5)
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the second topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check third topping
        if (toppingType3 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType3 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType3 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType3 == 4)
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType3 == 5)
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the third topping choice.");
          getToppingChoice(toppingAdds);
        }
        return toppingList;
      }
      // four toppings
      if (toppingAdds == 4)
      {
        System.Console.WriteLine("Enter the type of topping you would like for the first topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType1 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the second topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType2 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the third topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType3 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the fourth topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType4 = int.Parse(Console.ReadLine());
        if (toppingType1 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType1 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType1 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType1 == 4)
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType1 == 5)
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the first topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check second topping
        if (toppingType2 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType2 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType2 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType2 == 4)
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType2 == 5)
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the second topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check third topping
        if (toppingType3 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType3 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType3 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType3 == 4)
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType3 == 5)
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the third topping choice.");
          getToppingChoice(toppingAdds);
        }
        //check fourth topping 
        if (toppingType4 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType4 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType4 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType4 == 4)
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType4 == 5)
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the fourth topping choice.");
          getToppingChoice(toppingAdds);
        }
        return toppingList;
      }
      //five toppings 
      if (toppingAdds == 5)
      {
        System.Console.WriteLine("Enter the type of topping you would like for the first topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType1 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the second topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType2 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the third topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType3 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the fourth topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType4 = int.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter the type of topping you would like for the fifth topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType5 = int.Parse(Console.ReadLine());
        if (toppingType1 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType1 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType1 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType1 == 4)
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType1 == 5)
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the first topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check second topping
        if (toppingType2 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType2 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType2 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType2 == 4)
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType2 == 5)
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the second topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check third topping
        if (toppingType3 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType3 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType3 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType3 == 4)
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType3 == 5)
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the third topping choice.");
          getToppingChoice(toppingAdds);
        }
        //check fourth topping 
        if (toppingType4 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType4 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType4 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType4 == 4)
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType4 == 5)
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the fourth topping choice.");
          getToppingChoice(toppingAdds);
        }
        //check fifth topping 
        if (toppingType5 == 1)
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add(1);
        }
        else if (toppingType5 == 2)
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add(2);
        }
        else if (toppingType5 == 3)
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add(3);
        }
        else if (toppingType5 == 4)
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add(4);
        }
        else if (toppingType5 == 5)
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add(5);
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the fifth topping choice.");
          getToppingChoice(toppingAdds);
        }
        return toppingList;
      }
      return toppingList;
    }

    //askForExtra checks the amount of the toppings (array size) then loops to store in the correct variables.
    //if the size is not 5 then ask the user if they would like extra specific toppings. 
    public static double getToppingPrice(List<int> toppingList)
    {
      List<double> PriceOfToppings = new List<double>();
      double totalToppingPrice = 0.0;
      double countToppings = 0.0;
      foreach (int topping in toppingList)
      {
        countToppings += 1;
      }
      totalToppingPrice = countToppings * 1.50;
      System.Console.WriteLine($"for toppings ${totalToppingPrice}");
      return totalToppingPrice;
    }
  }
}
