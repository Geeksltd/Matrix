*Namespace: **System***
# DateTime.AddMilliseconds(*double*) ➜ *returns DateTime*
Returns a new DateTime that adds the specified number of AddMilliseconds to the value of this instance.

## Normal Scenarios
|Object|Call|Result|Remarks|
|---|---|---|---|
|3/1/2017 04:00:00.0000000  | .AddMilliseconds(1)  | ➜ 3/1/2017 04:00:00.0010000|
|3/1/2017 04:00:00.0000000  | .AddMilliseconds(1.5)  | ➜ 3/1/2017 04:00:00.0020000| The fractional part of value is the fractional part of a millisecond. 
|3/1/2017 04:00:00.0000000  | .AddMilliseconds(0)  | ➜ 3/1/2017 04:00:00.0000000| No change. No error.|
|3/1/2017 04:00:00.0000000  | .AddMilliseconds(-1)  | ➜ 3/1/2017 03:59:59.9990000| Negative works too|

## Special Scenarios
|Object|Call|Result|Remarks|
|---|---|---|---|
| 01/01/0001 12:00:00.0000000 | .AddMilliseconds(-1)  | ➜ *ArgumentOutOfRangeException* | Can't go below DateTime.MinValue|
| 12/31/9999 11:59:59.9999999 | .AddMilliseconds(1)  | ➜ *ArgumentOutOfRangeException* | Can't go above DateTime.MaxValue|
| 09/08/2017 04:00:00.0000000  | .AddMilliseconds(0.5)  | ➜ 12/31/9999 11:59:59.0010000 | fractional milliseconds are rounded.Here 0.5 is equivalent to 5000 ticks,but it is rounded to one millisecond = 10000 ticks.|

## Remarks
- It returns a new DateTime, but does not change the value of this DateTime (as it's immutable anyway).
- fractional milliseconds are rounded before performing the addition; For example, 1.5 is equivalent to 1 milliseconds and 5000 ticks, where one millisecond = 10000 ticks.