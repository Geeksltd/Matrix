*Namespace: **System***
# DateTime.AddDays(*double*) ➜ *returns DateTime*
Returns a new DateTime that adds the specified number of days to the value of this instance.

## Normal Scenarios

|Object|Call|Result|Remarks|
|---|---|---|---|
| 01 Jan 2010  | .AddDays(2)  | ➜ 03 Jan 2010|
| 01 Jan 2010  | .AddDays(2.5)  | ➜ 03 Jan 2010 @ 12pm| The fractional part of value is the fractional part of a day. For example, 2.5 is equivalent to 2 days and 12 hours.|
| 01 Jan 2010  | .AddDays(0)  | ➜ 01 Jan 2010| No change. No error.|
| 01 Jan 2010  | .AddDays(-1)  | ➜ 31 Dec 2009| Negative works too|

## Special Scenarios
|Object|Call|Result|Remarks|
|---|---|---|---|
| 01 Jan 0001  | .AddDays(-1)  | ➜ *ArgumentOutOfRangeException* | Can't go below DateTime.MinValue|
| 31 Dec 9999  | .AddDays(1)  | ➜ *ArgumentOutOfRangeException* | Can't go above DateTime.MaxValue|
| 01 Jan 2010  | .AddDays(1.000000001)  | ➜ 02 Jan 2010 | The value parameter is rounded to the **nearest millisecond** which means any ticks smaller than a millisecond are ignored.|

## Remarks
- It returns a new DateTime, but does not change the value of this DateTime (as it's immutable anyway).
- It takes into account **leap years** and the number of days in a month when performing date arithmetic.

