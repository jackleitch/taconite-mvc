using System;
using System.Collections.Generic;
using System.ComponentModel;
using TaconiteMvc.Syntax;

namespace TaconiteMvc.Infrastructure
{
  internal class SetAttributeCommandBuilder : ISetAttributeCommandForTargetSyntax
  {
    private readonly IDictionary<string, string> _attributes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    
    /// <summary>
    /// The <see cref="TaconiteResult"/> to which this command will be added.
    /// </summary>
    protected TaconiteResult Result { get; set; }

    public SetAttributeCommandBuilder(TaconiteResult result, string name, object value)
    {
      if (result == null)
        throw new ArgumentNullException("result");
      if (String.IsNullOrEmpty(name))
        throw new ArgumentNullException("name");
      if (value == null)
        throw new ArgumentNullException("value");

      Result = result;
      _attributes[name] = Convert.ToString(value);
    }

    public SetAttributeCommandBuilder(TaconiteResult result, object attributes)
    {
      if (result == null)
        throw new ArgumentNullException("result");
      if (attributes == null)
        throw new ArgumentNullException("attributes");

      Result = result;

      var properties = TypeDescriptor.GetProperties(attributes);
      if (properties.Count == 0)
        throw new ArgumentException("Object has no properties");

      foreach (PropertyDescriptor property in properties)
      {
        var attributeName = property.Name.Replace('_', '-');
        
        if (_attributes.ContainsKey(attributeName))
          throw new ArgumentException("Object contains duplicate attributes having name '{0}'", property.Name);

        _attributes[attributeName] = Convert.ToString(property.GetValue(attributes));
      }
    }

    /// <summary>
    /// Specifies the jQuery selector for the target element(s).
    /// </summary>
    /// <param name="selector">The jQuery selector for the target element(s).</param>
    TaconiteResult ISetAttributeCommandForTargetSyntax.For(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      foreach (var attribute in _attributes)
      {
        var command = new NonElementCommand("attr", selector, attribute.Key, attribute.Value);
        Result.AddCommand(command);
      }

      return Result;
    }
  }
}