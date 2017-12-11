# Int.CompareTo() ➜ *returns -1, 0 or 1*
Returns a number(-1,0,1) depending on what are the values of two input parameters.

## Examples
|Object|Call|Result|Remarks|
|---|---|---|---|
| -1000| .CompareTo(-1000)  | ➜ 0 | The 'this' value equals to second one
| 0   | .CompareTo(0)  | ➜ 0 | 
| 0  | .CompareTo(null)  | ➜ 1 | 
| 1000  | .CompareTo(-1000)  | ➜ 1 | 
| -1000  | .CompareTo(1000)  | ➜ -1 | 
 
## Special Cases
|Object|Call|Result|Remarks|
|---|---|---|---|
| -1000 | .CompareTo(1000000000)  | ➜ -1 | A big value
