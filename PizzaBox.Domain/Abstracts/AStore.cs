using System.Xml.Serialization;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
  /// <summary>
  /// Represents the Store Abstract Class
  /// </summary>
  [XmlInclude(typeof(ChicagoStore))]
  [XmlInclude(typeof(NewYorkStore))]
  public class AStore
  {
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// 
    /// changed Astore from protected to public so exceptions could be handled
    public AStore()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return $"{Name}";
    }
  }
}
