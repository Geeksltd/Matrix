# Number.CompareTo() ➜ *returns -1, 0 or 1*
Returns a number(-1,0,1) depending on what are the values of two input parameters.

## Examples
|Object|Call|Result|Remarks|
|---|---|---|---|
| -1000 and -1000 | .CompareTo(-1000,-1000)  | ➜ 0 | first value equals to second one
| 0 and 0 | .CompareTo(0,0)  | ➜ 0 | 
| 0 and null | .CompareTo(0,null)  | ➜ 1 | 
| null and 0 | .CompareTo(null,0)  | ➜ -1 | 
| null and -10 | .CompareTo(null,-10)  | ➜ -1 | first value is null and second value is less than 0
| null and 10 | .CompareTo(null,10)  | ➜ 1 | first value is null and second value is more than 0
| 1000 and -1000 | .CompareTo(1000,-1000)  | ➜ 1 | 
| -1000 and 1000 | .CompareTo(-1000,1000)  | ➜ -1 | 
 
## Special Cases
|Object|Call|Result|Remarks|
|---|---|---|---|
| null and null | .CompareTo(null,null)  | ➜ 0 |  both values equal to null
| -1000, 1000000000 | .CompareTo(-1000,)  | ➜ -1 | 
