# Platinum.PropertyGrid #

The Platinum.PropertyGrid control is aimed at providing a more flexible generic object editor than System.Windows.Forms.PropertyGrid. Like with the standard PropertyGrid users can implement their own PropertyEditors.

## Why Yet Another PropertyGrid ##
The main reasons for developing this control instead of just using the standard PropertyGrid where
  * the need for **real-time feedback** in PropertyEditors
  * **no direct property manipulation** through PropertyEditors
  * complete **control over the PropertyEditor area** (no forced dropdown or modal dialogs)
  * more **flexible PropertyGrid-population**

### Real-Time Feedback ###
In the default PropertyEditors already delivered with the library you will see that invalid values are indicated immediately as you type in a way that does not interupt your workflow by requiring to close an error dialog or commit the value to see if it is valid. In my opinion this greatly improves usability.

### No Direct Property Manipulation ###
Platinum.PropertyEditors must implement something like **transactional semantics**. They must support **commiting** the property value, **reverting** it and in between they must send **changing** events. This is meant to facilitate visualizing property changes in real-time without flooding the environment with undo-entries. Any property modifications are published to clients of the PropertyEditor only through C# events. Properties are not modified directly.

### Complete Property Editor Control ###
In some cases allowed property values lie within a small range making it more practical to use a NumericUpDown control for example instead of a simple string based editor. In the standard PropertyGrid control such a NumericUpDown control could only be placed in a dropdown window, which required the user one more click than necessary.

### Flexible PropertyGrid Population ###
At last the Platinum.PropertyGrid does not handle its population with property entries itself. Instead it can be filled from the outside. A DefaultPropertyGridFiller is provided that acts similar to the standard PropertyGrid filler. But it is also possible to implement custom fillers. For example an object that is only an aggregation of other objects can be displayed in the Platinum.PropertyGrid with one PropertySection (or "Category" in standard PropertyGrid terminology) for each object contained.
To a certain degree this functionality can also be achieved with the standard PropertyGrid by implementing custom type descriptors and proxy classes for selected objects but this is cumbersome and had some hard to circumvent restrictions.