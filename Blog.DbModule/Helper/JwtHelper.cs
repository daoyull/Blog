using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Common.Lib.Helpers;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Blog.DbModule.Helper;

public class JwtHelper
{
    //RSA私钥
    private const string PrivateKey = @"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
S2xOANqreXnJutzv4tVJGjp1I7YmDeU=";

    //RSA公钥
    private const string PublicKey = @"MIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEAuXGzXP/GHpe6U0+xWQqy
Bs1qBBzP3OW1bVBU6sQqKU1Ef5IjK8Qoej4d7mjYQk5keq9OOu2d4WnVp5SiqqJQ
wCOY74dBFa5JhAGvlW1gk3hewtwtGFtq0LfLtlTzs1QFeqX+IWZoKksLfK6yZn6w
1Ol1tv2UYeHuTLwM0zcaEqyHwUl1+B2gc86zu2evv9TxQWZgIEemrHJu7QQL59Z4
BRE15v7kHUc/mLq9uYFT+a2Ftq7mcQWgfIphTGs4SZzKV0vRLwWsERSwyfrs4YyD
L0NvqswWq3NNJF0ascc3/TUor+rjHi98L+Shi+Ga1ItMCKXbjnVcAXSwX47c8gL5
qPZkl0r9Csbp3vK0Ukc91ZvkTYZ7IDCbqHU7db7QsdtVyz92vJu45cJbGuwDGqwq
P0ExYKbf5zAVXv25Gd3aBLRt/s7sQOOWWNn1XbIdtpOdiD124ZiY1tA7J+BqHV4E
Wlu0azu6lS7q1GSfMPiK/i6ltB1H7xVaW9Mw9PxHr/ZtO/mZ1B4XDeoMSCWv1wgS
zk+U03AB/qF8Z4OeIZhhPZ4fqCfhQKEfT7sZInCcFzhFr3gMlWN5+THcFBlcLi+f
ntnsMEYYXBw8UKB3OZV0u8ZJ0xp6/yj+45lRgBU6gVCW81elhgdE65LBUlsEy+UI
0Eeb2Hq73mdGpgzQ/Z1w0WcCAwEAAQ==";

    public static string GetJwtToken(string guid)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, guid)
        };
        var rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(PrivateKey), out _);
        SecurityKey securityKey = new RsaSecurityKey(rsa);
        var token = new JwtSecurityToken(
            issuer: "isle",
            audience: "isle",
            claims: claims,
            notBefore: TimeHelper.NowCst,
            expires: TimeHelper.NowCst.AddHours(6),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256)
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }

    public static bool VerifyJwtToken(string token, out ClaimsPrincipal? principal)
    {
        principal = null;
        //校验token
        var validateParameter = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "isle",
            ValidAudience = "isle",
            IssuerSigningKey = new RsaSecurityKey(CreateRsaProviderFromPublicKey(PublicKey)),

            ClockSkew = TimeSpan.Zero //校验过期时间必须加此属性
        };
        bool success;
        try
        {
            //校验并解析token,validatedToken是解密后的对象
            principal = new JwtSecurityTokenHandler().ValidateToken(token, validateParameter,
                out _);
            //获取payload中的数据 
            //var jwtPayload = ((JwtSecurityToken)validatedToken).Payload.SerializeToJson(); 
            success = true;
        }
        catch (SecurityTokenExpiredException)
        {
            //表示过期
            success = false;
        }
        catch (SecurityTokenException)
        {
            //表示token错误
            success = false;
        }
        catch (Exception)
        {
            success = false;
        }

        return success;
    }


    #region 私有方法

    /// <summary>
    /// 通过公钥创建RSA
    /// </summary>
    /// <param name="publicKeyString"></param>
    /// <returns></returns>
    private static RSA? CreateRsaProviderFromPublicKey(string publicKeyString)
    {
        // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
        byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        byte[] seq;
        var x509Key = Convert.FromBase64String(publicKeyString);
        // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
        using (MemoryStream mem = new MemoryStream(x509Key))
        {
            using (BinaryReader binr = new BinaryReader(mem)) //wrap Memory Stream with BinaryReader for easy reading
            {
                byte bt;
                ushort twobytes;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte(); //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16(); //advance 2 bytes
                else
                    return null;

                seq = binr.ReadBytes(15); //read the Sequence OID
                if (!CompareBytearrays(seq, seqOid)) //make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte(); //advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16(); //advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00) //expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte(); //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16(); //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte(); // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;

                byte[] modint =
                    { lowbyte, highbyte, 0x00, 0x00 }; //reverse byte order since asn.1 key uses big endian order
                int modsize = BitConverter.ToInt32(modint, 0);

                int firstbyte = binr.PeekChar();
                if (firstbyte == 0x00)
                {
                    //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte(); //skip this null byte
                    modsize -= 1; //reduce modulus buffer size by 1
                }

                byte[] modulus = binr.ReadBytes(modsize); //read the modulus bytes

                if (binr.ReadByte() != 0x02) //expect an Integer for the exponent data
                    return null;
                int expbytes =
                    binr.ReadByte(); // should only need one byte for actual exponent data (for all useful values)
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                var rsa = RSA.Create();
                RSAParameters rsaKeyInfo = new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = exponent
                };
                rsa.ImportParameters(rsaKeyInfo);

                return rsa;
            }
        }
    }

    private static bool CompareBytearrays(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
            return false;
        int i = 0;
        foreach (byte c in a)
        {
            if (c != b[i])
                return false;
            i++;
        }

        return true;
    }

    #endregion

    public static bool GetGuidByToken(string token, out string guid)
    {
        guid = string.Empty;
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        if (!VerifyJwtToken(token, out var principal))
        {
            return false;
        }

        guid = principal!.Claims.First(it => it.Type == "jti").Value;
        return true;
    }
}