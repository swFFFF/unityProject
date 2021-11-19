print("---------------字符串-----------------")
str = "双引号"
str1 = '单引号'

--获取字符串长度
s = "abBeDs施213(。"
--中文字符占3个长度
--英文字符占1个长度
print(#s)
--lua支持转义字符
print("123\n123")
s = [[我是 
123	
哈哈]]
print(s)
--字符串拼接
--通过.. 类似c# +
print("123".."234")
d = true
e = 123
print("123" .. e)

--格式化输出和c++类似
--%d 数字拼接
--%a 任何字符拼接
--%s 字符配对
print(string.format("aA%s\0","Ss","%a"))

--别的类型转字符串
a = true
print(tostring(a))
str = "aAsbB"
print(string.upper(str))
print(string.lower(str))
--翻转字符串
print(string.reverse(str))
--字符串索引查找 下标从1开始计算
print(string.find(str, "s"))
--截取字符串
print(string.sub(str, 3, 4))
--字符串重复
print(string.rep(str, 2))
--字符串修改
print(string.gsub(str, "s", "**"))
--字符转ASCII码
a = string.byte("Lua",1)
print(a)
--ASCII码转字符
print(string.char(a))