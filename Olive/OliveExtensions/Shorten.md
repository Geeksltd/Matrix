*Namespace: **Olive***
# Guid.Shorten() ➜ *returns ShortGuid*
Returns a ShortGuid object which provides a shorter string equivalent than the normal Guid.
When you convert a normal Guid to string, it's **32 characters**, while a ShortGuid is only **22 characters**.

## Remarks
- Normal guid uses the characters '0' to '9', 'a' to 'f'. This is a range of 16 different characters. It also has generally 4 dashes that contain no data.
- ShortGuid uses the characters '0' to '9', 'a' to 'z', 'A' to 'Z' and also '-' and '_'. This is a range of 64 characters.
- Unlike normal Guid, the string value of ShortGuid is **case-sensitive**.

## Examples

|Object|Call|Result|Remarks|
|---|---|---|---|
| d7fedc56-959f-4d5b-8855-6138b534bce4 | .Shorten()  | ➜ Vtz-15-VW02IVWE4tTS85A | 
| aceefe63-42f3-4135-a63a-96d1636f3b8d | .Shorten()  | ➜ Y_7urPNCNUGmOpbRY287jQ | 
| 00000000-0000-0000-0000-000000000000 | .Shorten()  | ➜ **TODO:????????** | The value of Guid.Empty
