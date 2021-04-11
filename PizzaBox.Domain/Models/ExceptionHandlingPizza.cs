namespace ExceptionHandlingPizza
{ //handles most of the errors from the PizzaBox.Client runs


  public class CrustInputException : System.Exception
  {
    public override string Message
    {
      get
      {
        return "Crust number was not selected. Please choose a crust : 0 soft, 1 cheesy, 2 handtossed, 3 deepdish";
      }
    }
  }

  public class PizzaSizeException : System.Exception
  {
    public override string Message
    {
      get
      {
        return "size number was not selected. Please choose a size : 0 small-10-inch  1 medium-12-inch  2 large-14-inch  3 extra-large-16-inch";
      }
    }
  }
}
