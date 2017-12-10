*Namespace: **System***
# DateTime.AddDays(*double*) ➜ *returns DateTime*
Returns a new DateTime that adds the specified number of days to the value of this instance.
## Remarks
- It does not change the value of this DateTime. Instead, it returns a new DateTime whose value is the result of this operation.
- The value parameter is rounded to the **nearest millisecond**.
- It takes into account **leap years** and the number of days in a month when performing date arithmetic.

## Examples

|Object|Call|Result|Remarks|
|---|---|---|---|
| 01 Jan 2010  | AddDays(2)  | ➜ 03 Jan 2010|
| 01 Jan 2010  | AddDays(2.5)  | ➜ 03 Jan 2010 @ 12pm| The fractional part of value is the fractional part of a day. For example, 2.5 is equivalent to 2 days and 12 hours.|
| 01 Jan 2010  | AddDays(0)  | ➜ 01 Jan 2010| No change. No error.|
| 01 Jan 2010  | AddDays(-1)  | ➜ 31 Dec 2009| Negative works too|

[More details](https://docs.microsoft.com/en-us/dotnet/api/system.datetime.adddays)
