using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Client.Singletons;
using ExceptionHandlingPizza;

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
      var basePizzaPrice = getPizzaPrice(order.Pizza);
      var crust = listCrusts();
      var crustPrice = getCrustPrice(crust);
      var size = listSizes();
      var sizePrice = getSizePrice(size);
      var Toppings = getNumberToppings();
      List<string> ToppingChoices = getToppingChoice(Toppings);
      var ToppingsPrice = getToppingPrice(ToppingChoices);
      DisplayOrder(order.Pizza, crust, size, ToppingChoices);
      DisplayOrderCost(basePizzaPrice, crustPrice, sizePrice, ToppingsPrice);

      //list of toppings

      order.Save();
    }

    private static void DisplayOrderCost(double basePizzaPrice, double crustPrice, double sizePrice, double toppingsPrice)
    {
      double finalPizzaPrice = basePizzaPrice + crustPrice + sizePrice + toppingsPrice;
      System.Console.WriteLine($"Your order total is : ${finalPizzaPrice}");
    }
    private static double getPizzaPrice(APizza pizza)
    {
      double pizzaBasePrice = 0.0;

      if (pizza == _pizzaSingleton.Pizzas[0])
      {
        pizzaBasePrice = 15.00;
      }
      else if (pizza == _pizzaSingleton.Pizzas[1])
      {
        pizzaBasePrice = 10.00;
      }
      System.Console.WriteLine($"${pizzaBasePrice} for {pizza}");
      return pizzaBasePrice;
    }
    /// <summary>
    /// 
    /// </summary> //will need to change this later
    private static void DisplayOrder(APizza pizza, string crust, string size, List<string> toppingList)
    {
      Console.WriteLine($"Your order is: {pizza}, {crust}, {size} with toppings: ");
      string[] toppingsArray = toppingList.ToArray();
      foreach (var toppings in toppingList)
      {
        System.Console.WriteLine(toppings);
      }

      //find a way to store the object APizza type, check with if else method and return price 
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
      //print the price of each pizza choice
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
      var pizza = _pizzaSingleton.Pizzas[0];
      int number;
      string input = Console.ReadLine();
      bool isTrue = int.TryParse(input, out number);
      if (isTrue)
      {
        if (number > 2) // this is comparing the size of the pizzas index, checks if number is out of bounds
        {
          System.Console.WriteLine("input " + number + " was not listed but defaulted to the last pizza");
          number = 2;
          pizza = _pizzaSingleton.Pizzas[number - 1];
        }

        if (number <= 0)// this is comparing the size of the pizzas index, checks if number is a zero or a negative value
        {
          System.Console.WriteLine("input " + number + " was not listed but defaulted to the first pizza");
          number = 1;
          pizza = _pizzaSingleton.Pizzas[number - 1];
        }
        pizza = _pizzaSingleton.Pizzas[number - 1];
      }
      else
      {
        System.Console.WriteLine("Incorrect input, please enter a number corresponding to a type of pizza. Received " + input + " " + number);
        DisplayPizzaMenu();
        SelectPizza();
      }

      //DisplayOrder(pizza);

      return pizza;
    }

    /// <summary>
    /// after you select a store then a menu is displayed
    /// </summary>
    /// <returns></returns>
    private static AStore SelectStore()
    {
      AStore theStore = new AStore();
      int number;
      string input = Console.ReadLine();
      bool isTrue = int.TryParse(input, out number); // be careful (think execpetion/error handling)
      if (isTrue)
      {
        if (number > 4) // this is comparing the size of the stores index, checks if number is out of bounds
        {
          System.Console.WriteLine("input " + number + " was not listed but defaulted to the last store");
          number = 4;
          theStore = _storeSingleton.Stores[number - 1];
        }

        if (number <= 0)// this is comparing the size of the stores index, checks if number is a zero or a negative value
        {
          System.Console.WriteLine("input " + number + " was not listed but defaulted to the first store");
          number = 1;
          theStore = _storeSingleton.Stores[number - 1];
        }
        theStore = _storeSingleton.Stores[number - 1];
      }
      else
      {
        System.Console.WriteLine("Incorrect input, please enter a number corresponding to a store. Received " + input + " " + number);
        DisplayStoreMenu();
        SelectStore();
      }

      DisplayPizzaMenu();

      return theStore;
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

      //string crustChoice = " ";
      var crustChoice = " ";

      try
      { //change to a string input instead 
        var crustSwitch = Console.ReadLine();
        switch (crustSwitch)
        {
          case "0":
            crustChoice = list[0];
            System.Console.WriteLine("crust is " + list[0]);
            break;

          case "1":
            crustChoice = list[1];
            System.Console.WriteLine("crust is " + list[1]);
            break;

          case "2":
            crustChoice = list[2];
            System.Console.WriteLine("crust is " + list[2]);
            break;

          case "3":
            crustChoice = list[3];
            System.Console.WriteLine("crust is " + list[3]);
            break;

          default:

            throw new CrustInputException();
            //System.Console.WriteLine("Crust number was not selected. Please choose a crust : 0 soft, 1 cheesy, 2 handtossed, 3 deepdish");
            //listCrusts();
            //break;
        }
      }
      catch (CrustInputException e)
      {
        System.Console.WriteLine(e.Message);
        //System.Console.WriteLine("Crust number was not selected. Please choose a crust : 0 soft, 1 cheesy, 2 handtossed, 3 deepdish");
        listCrusts();
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
      try
      {
        var sizeSwitch = Console.ReadLine();
        switch (sizeSwitch)
        {
          case "0":
            sizeChoice = list[0];
            System.Console.WriteLine("size is " + list[0]);
            break;

          case "1":
            sizeChoice = list[1];
            System.Console.WriteLine("size is " + list[1]);
            break;

          case "2":
            sizeChoice = list[2];
            System.Console.WriteLine("size is " + list[2]);
            break;

          case "3":
            sizeChoice = list[3];
            System.Console.WriteLine("size is " + list[3]);
            break;

          default:
            throw new PizzaSizeException();
            //System.Console.WriteLine("size number was not selected. Please choose a size : 0 small-10-inch  1 medium-12-inch  2 large-14-inch  3 extra-large-16-inch");
            //listCrusts();
            //break;
        }
      }
      catch (PizzaSizeException e)
      {
        System.Console.WriteLine(e.Message);
        listSizes();
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
      var toppingTypeNum = " ";
      var toppingAdds = 0;
      System.Console.WriteLine("Enter how many toppings you would like. You can have up to five and no less than two. Enter 2 for two toppings, 3 for three, 4 for four or 5 for five.");
      toppingTypeNum = Console.ReadLine();
      if (toppingTypeNum.Equals("2"))
      {
        toppingAdds = 2;
      }
      else if (toppingTypeNum.Equals("3"))
      {
        toppingAdds = 3;
      }
      else if (toppingTypeNum.Equals("4"))
      {
        toppingAdds = 4;
      }
      else if (toppingTypeNum.Equals("5"))
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
    public static List<string> getToppingChoice(int toppingAdds)
    {
      List<string> toppingList = new List<string>();
      var toppingType1 = " ";
      var toppingType2 = " ";
      var toppingType3 = " ";
      var toppingType4 = " ";
      var toppingType5 = " ";
      // one topping
      /*if (toppingAdds == 1)
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
      }*/
      //two toppings
      if (toppingAdds == 2)
      {
        System.Console.WriteLine("Enter the type of topping you would like for the first topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType1 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the second topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType2 = Console.ReadLine();
        if (toppingType1.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType1.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType1.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType1.Equals("4"))
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType1.Equals("5"))
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the first topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check second topping
        if (toppingType2.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType2.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType2.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType2.Equals("4"))
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType2.Equals("5"))
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add("cheese");
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
        toppingType1 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the second topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType2 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the third topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType3 = Console.ReadLine();
        if (toppingType1.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType1.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType1.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType1.Equals("4"))
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType1.Equals("5"))
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the first topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check second topping
        if (toppingType2.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType2.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType2.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType2.Equals("4"))
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType2.Equals("5"))
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the second topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check third topping
        if (toppingType3.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType3.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType3.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType3.Equals("4"))
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType3.Equals("5"))
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add("cheese");
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
        toppingType1 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the second topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType2 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the third topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType3 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the fourth topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType4 = Console.ReadLine();
        if (toppingType1.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType1.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType1.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType1.Equals("4"))
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType1.Equals("5"))
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the first topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check second topping
        if (toppingType2.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType2.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType2.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType2.Equals("4"))
        {
          System.Console.WriteLine("You chose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType2.Equals("5"))
        {
          System.Console.WriteLine("You chose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the second topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check third topping
        if (toppingType3.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType3.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType3.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType3.Equals("4"))
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType3.Equals("5"))
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the third topping choice.");
          getToppingChoice(toppingAdds);
        }
        //check fourth topping 
        if (toppingType4.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType4.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType4.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType4.Equals("4"))
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType4.Equals("5"))
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add("cheese");
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
        toppingType1 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the second topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType2 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the third topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType3 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the fourth topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType4 = Console.ReadLine();
        System.Console.WriteLine("Enter the type of topping you would like for the fifth topping: 1 for pepporoni, 2 for saugsage, 3 for olives, 4 for peppers, 5 for cheese");
        toppingType5 = Console.ReadLine();
        if (toppingType1.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType1.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType1.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType1.Equals("4"))
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType1.Equals("5"))
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the first topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check second topping
        if (toppingType2.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType2.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType2.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType2.Equals("4"))
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType2.Equals("5"))
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the second topping choice.");
          getToppingChoice(toppingAdds);
        }
        // check third topping
        if (toppingType3.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType3.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType3.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType3.Equals("4"))
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType3.Equals("5"))
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the third topping choice.");
          getToppingChoice(toppingAdds);
        }
        //check fourth topping 
        if (toppingType4.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType4.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType4.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType4.Equals("4"))
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType4.Equals("5"))
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add("cheese");
        }
        else
        {
          System.Console.WriteLine("You did not enter a number for the fourth topping choice.");
          getToppingChoice(toppingAdds);
        }
        //check fifth topping 
        if (toppingType5.Equals("1"))
        {
          System.Console.WriteLine("You chose pepporoni.");
          toppingList.Add("pepporoni");
        }
        else if (toppingType5.Equals("2"))
        {
          System.Console.WriteLine("You chose saugsage.");
          toppingList.Add("suasage");
        }
        else if (toppingType5.Equals("3"))
        {
          System.Console.WriteLine("You chose olives.");
          toppingList.Add("olives");
        }
        else if (toppingType5.Equals("4"))
        {
          System.Console.WriteLine("You choose peppers.");
          toppingList.Add("peppers");
        }
        else if (toppingType5.Equals("5"))
        {
          System.Console.WriteLine("You choose cheese.");
          toppingList.Add("cheese");
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
    public static void getToppingList(List<string> toppingList)
    {
      string[] toppingsArray = toppingList.ToArray();
      foreach (var toppings in toppingList)
      {
        System.Console.WriteLine(toppings);
      }
    }
    //askForExtra checks the amount of the toppings (array size) then loops to store in the correct variables.
    //if the size is not 5 then ask the user if they would like extra specific toppings. 
    public static double getToppingPrice(List<string> toppingList)
    {
      List<double> PriceOfToppings = new List<double>();
      string[] pricingArray = toppingList.ToArray();
      toppingList.Add(" ");

      string topping2 = " ";
      string topping3 = " ";
      string topping4 = " ";
      string topping5 = " ";
      string topping6 = " ";

      int numOfToppings = pricingArray.Length;
      //errors here toppings are accessed outside the array (check size with if else then assign otherwise index out of bounds error)
      if (numOfToppings == 2)
      {
        topping2 = pricingArray[0];
        topping3 = pricingArray[1];
      }
      else if (numOfToppings == 3)
      {
        topping2 = pricingArray[0];
        topping3 = pricingArray[1];
        topping4 = pricingArray[2];
      }
      else if (numOfToppings == 4)
      {
        topping2 = pricingArray[0];
        topping3 = pricingArray[1];
        topping4 = pricingArray[2];
        topping5 = pricingArray[3];
      }
      else if (numOfToppings == 5)
      {
        topping2 = pricingArray[0];
        topping3 = pricingArray[1];
        topping4 = pricingArray[2];
        topping5 = pricingArray[3];
        topping6 = pricingArray[4];
      }

      double pepporoniPrice = 2.00;
      double saugsagePrice = 3.00;
      double olivesPrice = 1.50;
      double peppersPrice = 1.75;
      double cheesePrice = 3.50;
      double totalToppingPrice = 0.0;
      //store each price so computation is correctly handled
      double toppingOnePriceAdd = 0.0;
      double toppingTwoPriceAdd = 0.0;
      double toppingThreePriceAdd = 0.0;
      double toppingFourPriceAdd = 0.0;
      double toppingFivePriceAdd = 0.0;


      if (numOfToppings == 2)
      {
        if (topping2.Equals("pepporoni"))
        {
          toppingOnePriceAdd += pepporoniPrice;
        }
        else if (topping2.Equals("suasage"))
        {
          toppingOnePriceAdd += saugsagePrice;
        }
        else if (topping2.Equals("olives"))
        {
          toppingOnePriceAdd += olivesPrice;
        }
        else if (topping2.Equals("peppers"))
        {
          toppingOnePriceAdd += peppersPrice;
        }
        else
        {
          toppingOnePriceAdd += cheesePrice;
        }
        //second topping price
        if (topping3.Equals("pepporoni"))
        {
          toppingTwoPriceAdd += pepporoniPrice;
        }
        else if (topping3.Equals("suasage"))
        {
          toppingTwoPriceAdd += saugsagePrice;
        }
        else if (topping3.Equals("olives"))
        {
          toppingTwoPriceAdd += olivesPrice;
        }
        else if (topping3.Equals("peppers"))
        {
          toppingTwoPriceAdd += peppersPrice;
        }
        else
        {
          toppingTwoPriceAdd += cheesePrice;
        }
      }
      // three toppings
      if (numOfToppings == 3)
      {
        if (topping2.Equals("pepporoni"))
        {
          toppingOnePriceAdd += pepporoniPrice;
        }
        else if (topping2.Equals("suasage"))
        {
          toppingOnePriceAdd += saugsagePrice;
        }
        else if (topping2.Equals("olives"))
        {
          toppingOnePriceAdd += olivesPrice;
        }
        else if (topping2.Equals("peppers"))
        {
          toppingOnePriceAdd += peppersPrice;
        }
        else
        {
          totalToppingPrice += cheesePrice;
        }
        //second topping price
        if (topping3.Equals("pepporoni"))
        {
          toppingTwoPriceAdd += pepporoniPrice;
        }
        else if (topping3.Equals("suasage"))
        {
          toppingTwoPriceAdd += saugsagePrice;
        }
        else if (topping3.Equals("olives"))
        {
          toppingTwoPriceAdd += olivesPrice;
        }
        else if (topping3.Equals("peppers"))
        {
          toppingTwoPriceAdd += peppersPrice;
        }
        else
        {
          toppingTwoPriceAdd += cheesePrice;
        }
        //third topping price

        if (topping4.Equals("pepporoni"))
        {
          toppingThreePriceAdd += pepporoniPrice;
        }
        else if (topping4.Equals("suasage"))
        {
          toppingThreePriceAdd += saugsagePrice;
        }
        else if (topping4.Equals("olives"))
        {
          toppingThreePriceAdd += olivesPrice;
        }
        else if (topping4.Equals("peppers"))
        {
          toppingThreePriceAdd += peppersPrice;
        }
        else
        {
          toppingThreePriceAdd += cheesePrice;
        }
      }
      // four toppings 
      if (numOfToppings == 4)
      {
        if (topping2.Equals("pepporoni"))
        {
          toppingOnePriceAdd += pepporoniPrice;
        }
        else if (topping2.Equals("suasage"))
        {
          toppingOnePriceAdd += saugsagePrice;
        }
        else if (topping2.Equals("olives"))
        {
          toppingOnePriceAdd += olivesPrice;
        }
        else if (topping2.Equals("peppers"))
        {
          toppingOnePriceAdd += peppersPrice;
        }
        else
        {
          toppingOnePriceAdd += cheesePrice;
        }
        //second topping price
        if (topping3.Equals("pepporoni"))
        {
          toppingTwoPriceAdd += pepporoniPrice;
        }
        else if (topping3.Equals("suasage"))
        {
          toppingTwoPriceAdd += saugsagePrice;
        }
        else if (topping3.Equals("olives"))
        {
          toppingTwoPriceAdd += olivesPrice;
        }
        else if (topping3.Equals("peppers"))
        {
          toppingTwoPriceAdd += peppersPrice;
        }
        else
        {
          toppingTwoPriceAdd += cheesePrice;
        }
        //third topping price
        if (topping4.Equals("pepporoni"))
        {
          toppingThreePriceAdd += pepporoniPrice;
        }
        else if (topping4.Equals("suasage"))
        {
          toppingThreePriceAdd += saugsagePrice;
        }
        else if (topping4.Equals("olives"))
        {
          toppingThreePriceAdd += olivesPrice;
        }
        else if (topping4.Equals("peppers"))
        {
          toppingThreePriceAdd += peppersPrice;
        }
        else
        {
          toppingThreePriceAdd += cheesePrice;
        }
        //fourth topping price
        if (topping5.Equals("pepporoni"))
        {
          toppingFourPriceAdd += pepporoniPrice;
        }
        else if (topping5.Equals("suasage"))
        {
          toppingFourPriceAdd += saugsagePrice;
        }
        else if (topping5.Equals("olives"))
        {
          toppingFourPriceAdd += olivesPrice;
        }
        else if (topping5.Equals("peppers"))
        {
          toppingFourPriceAdd += peppersPrice;
        }
        else
        {
          toppingFourPriceAdd += cheesePrice;
        }
      }
      // five toppings 
      if (numOfToppings == 5)
      {
        if (topping2.Equals("pepporoni"))
        {
          toppingOnePriceAdd += pepporoniPrice;
        }
        else if (topping2.Equals("suasage"))
        {
          toppingOnePriceAdd += saugsagePrice;
        }
        else if (topping2.Equals("olives"))
        {
          toppingOnePriceAdd += olivesPrice;
        }
        else if (topping2.Equals("peppers"))
        {
          toppingOnePriceAdd += peppersPrice;
        }
        else
        {
          toppingOnePriceAdd += cheesePrice;
        }
        //second topping price
        if (topping3.Equals("pepporoni"))
        {
          toppingTwoPriceAdd += pepporoniPrice;
        }
        else if (topping3.Equals("suasage"))
        {
          toppingTwoPriceAdd += saugsagePrice;
        }
        else if (topping3.Equals("olives"))
        {
          toppingTwoPriceAdd += olivesPrice;
        }
        else if (topping3.Equals("peppers"))
        {
          toppingTwoPriceAdd += peppersPrice;
        }
        else
        {
          toppingTwoPriceAdd += cheesePrice;
        }
        //third topping price
        if (topping4.Equals("pepporoni"))
        {
          toppingThreePriceAdd += pepporoniPrice;
        }
        else if (topping4.Equals("suasage"))
        {
          toppingThreePriceAdd += saugsagePrice;
        }
        else if (topping4.Equals("olives"))
        {
          toppingThreePriceAdd += olivesPrice;
        }
        else if (topping4.Equals("peppers"))
        {
          toppingThreePriceAdd += peppersPrice;
        }
        else
        {
          toppingThreePriceAdd += cheesePrice;
        }
        //fourth topping price
        if (topping5.Equals("pepporoni"))
        {
          toppingFourPriceAdd += pepporoniPrice;
        }
        else if (topping5.Equals("suasage"))
        {
          toppingFourPriceAdd += saugsagePrice;
        }
        else if (topping5.Equals("olives"))
        {
          toppingFourPriceAdd += olivesPrice;
        }
        else if (topping5.Equals("peppers"))
        {
          toppingFourPriceAdd += peppersPrice;
        }
        else
        {
          toppingFourPriceAdd += cheesePrice;
        }
        //fifth topping price
        if (topping6.Equals("pepporoni"))
        {
          toppingFivePriceAdd += pepporoniPrice;
        }
        else if (topping6.Equals("suasage"))
        {
          toppingFivePriceAdd += saugsagePrice;
        }
        else if (topping6.Equals("olives"))
        {
          toppingFivePriceAdd += olivesPrice;
        }
        else if (topping6.Equals("peppers"))
        {
          toppingFivePriceAdd += peppersPrice;
        }
        else
        {
          toppingFivePriceAdd += cheesePrice;
        }
      }
      totalToppingPrice = toppingOnePriceAdd + toppingTwoPriceAdd + toppingThreePriceAdd + toppingFourPriceAdd + toppingFivePriceAdd;
      System.Console.WriteLine($"for toppings ${totalToppingPrice}");
      return totalToppingPrice;
    }
  }
}
