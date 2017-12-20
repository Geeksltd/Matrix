*Namespace: **System***
# DateTime.AddHours(*double*) ➜ *returns DateTime*
Returns a new DateTime that adds the specified number of hours to the value of this instance.

## Normal Scenarios
the following output on a system whose current culture is en-US:

|Object|Call|Result|Remarks|
|---|---|---|---|
| 3/1/2017 12:00:00 PM  | .AddHours(2)  | ➜ 3/1/2017 2:00:00 PM|
| 3/1/2017 12:00:00 PM  | .AddHours(0.016667)  | ➜ 3/1/2017 12:01:00 PM| The fractional part of value is the fractional part of an hour. For example, 4.5 is equivalent to 4 hours, 30 minutes, 0 seconds, 0 milliseconds, and 0 ticks.One minute is 0.016667 of an hour.
| 3/1/2017 12:00:00 PM  | .AddHours(0)  | ➜ 3/1/2017 12:00:00 PM| No change. No error.|
| 3/1/2017 12:00:00 PM  | .AddHours(-1)  | ➜ 3/1/2017 11:00:00 AM| Negative works too|

## Special Scenarios
|Object|Call|Result|Remarks|
|---|---|---|---|
| 1/1/0001 12:00:00 AM  | .AddHours(-1)  | ➜ *ArgumentOutOfRangeException* | Can't go below DateTime.MinValue|
| 12/31/9999 11:59:59 PM  | .AddHours(1)  | ➜ *ArgumentOutOfRangeException* | Can't go above DateTime.MaxValue|
| 01 Jan 2010  | .AddHours(1.000000001)  | ➜ 02 Jan 2010 | The value parameter is rounded to the **nearest millisecond** which means any ticks smaller than a millisecond are ignored.|

## Remarks
- It returns a new DateTime, but does not change the value of this DateTime (as it's immutable anyway).
- Converting time intervals of less than an hour to a fraction can involve a loss of precision if the result is a non-terminating repeating decimal. (For example, one minute is 0.016667 of an hour.) If this is problematic, you can use the Add method, which enables you to specify more than one kind of time interval in a single method call and eliminates the need to convert time intervals to fractional parts of an hour.