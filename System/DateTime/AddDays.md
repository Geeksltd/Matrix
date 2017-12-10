# DateTime.AddDays(*double*)
Namespace: **System**

Returns a new DateTime that adds the specified number of days to the value of this instance.
- This method does not change the value of this DateTime. Instead, it returns a new DateTime whose value is the result of this operation.
- The fractional part of value is the fractional part of a day. For example, 4.5 is equivalent to 4 days, 12 hours, 0 minutes, 0 seconds, 0 milliseconds, and 0 ticks.
- The value parameter is rounded to the nearest millisecond.
- The AddDays method takes into account leap years and the number of days in a month when performing date arithmetic.

# Examples

|Object|Call|Result|Notes|
|---|---|---|---|
| 01 Jan 2010  | AddDays(2)  | 03 Jan 2010|
| 01 Jan 2010  | AddDays(2.5)  | 03 Jan 2010 @ 12pm| for the 0.5 part, 12 hours have been added.|
| 01 Jan 2010  | AddDays(0)  | 01 Jan 2010| No change. No error.|
| 01 Jan 2010  | AddDays(-1)  | 31 Dec 2009| Negative works too|

[More details](https://docs.microsoft.com/en-us/dotnet/api/system.datetime.adddays)
