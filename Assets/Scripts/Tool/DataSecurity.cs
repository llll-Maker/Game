using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class DataSecurity : MonoBehaviour
{
    string key = "12345678123456781234567812345678";//256位的密钥

    /// <summary>
    /// 字符串的加密
    /// </summary>
    /// <param name="toE"></param>
    /// <returns></returns>
    public string Encript(string toE)
    {
        //将密钥转换为byte数组
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
        //创建 RijndaelManaged对象
        RijndaelManaged rDel = new RijndaelManaged();
        //设置相关参数
        rDel.Key = keyArray;//密钥
        rDel.Mode = CipherMode.ECB;//快码模式
        rDel.Padding = PaddingMode.PKCS7;//填充模式
        //创建加密器对象
        ICryptoTransform cryptoTransform = rDel.CreateEncryptor();
        //将要加密的明文字符串转换为byte数组
        byte[] toEncriptsArray = UTF8Encoding.UTF8.GetBytes(toE);
        //加密 得到密文的byte数组
        byte[] resultArray = cryptoTransform.TransformFinalBlock(toEncriptsArray, 0, toEncriptsArray.Length);
        //返回密文字符串
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// 字符串解密
    /// </summary>
    /// <param name="toD"></param>
    /// <returns></returns>
    public string Decript(string toD)
    {
        //将密钥转换为byte数组
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
        //创建 RijndaelManaged对象
        RijndaelManaged rDel = new RijndaelManaged();
        //设置相关参数
        rDel.Key = keyArray;//密钥
        rDel.Mode = CipherMode.ECB;//快码模式
        rDel.Padding = PaddingMode.PKCS7;//填充模式
        //创建解密器对象
        ICryptoTransform ctransform = rDel.CreateDecryptor();
        //将密文字符串转换为byte数组
        byte[] toDecriptsArray = Convert.FromBase64String(toD);
        //解密，得到明文byte的数组
        byte[] resultArray = ctransform.TransformFinalBlock(toDecriptsArray,0 , toDecriptsArray.Length);
        //返回明文字符串
        return UTF8Encoding.UTF8.GetString(resultArray, 0, resultArray.Length);
    }
}
