using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.Resources
{
	public enum CitiesEnum
	{
		BeiJing = 202,
		ChangSha = 461,
		ChongQing = 51,
		FuAn = 472,
		XiaMen = 476,
		LanZhou = 602,
		QingYang = 722,
		NeiMongol = 205,
		GuiYang = 392,
		NingXia = 211,
		JiNan = 212,
		QingDao = 516,
		HaErBing = 584,
		DaTong = 661,
		ChangZhi = 662,
		XiAn = 601,
		GuangZhou = 217,
		FoShan = 262,
		DongGuan = 263,
		ZhengZhou = 271,
		NanYang = 701,
		ShangHai = 280,
		JingAn = 245,
		YuXi = 563,
		WuHan = 259,
		YiChang = 444,
		HeFei = 609,
		HuangShan = 611,
		XiZang = 274,
		NanChang = 275,
		JiuJiang = 402,
		TianJing = 279,
		TangShan = 289,
		BaoDing = 294,
		XinJiang = 390,
		Ulumuqi = 719,
		DaLian = 443,
		ShenYang = 582,
		Hengyang = 463,
		ChangChun = 587,
		LiuZhou = 531,
		NanNing = 532,
		GuangXi = 533,
		YiBing = 538,
		ChengDu = 539,
		HaiKou = 557,
		WenChang = 676,
		HangZhou = 573,
		WenZhou = 580,
		XiNing = 574,
		NanJing = 590,
		NingBo = 732,
		QingYuan = 642,
		SanYa = 679,
		ShenZhen = 264,
		SuZhou = 595,
		TaiYuan = 595,
		ZhanJiang = 635,
		ZhaoQing = 643,
		ZhongShan = 268,
		ZhuHai = 267
	}

	public enum ProvincesEnum
	{
		Hongkong = 12,
		ChongQing = 49,
		FuJian = 79,
		GanSu = 80,
		BeiJing = 180,
		NelMongo = 183,
		Taiwan = 184,
		GuiZhou = 188,
		NinXia = 189,
		ShanDong = 190,
		HeiLongJiang = 192,
		ShanXi = 193,
		ShaanXi = 194,
		GuangDong = 195,
		HeNan = 196,
		ShangHai = 221,
		YunNan = 227,
		HuBei = 235,
		AnHui = 236,
		XiZang = 238,
		JiangXi = 239,
		Macau = 241,
		TianJing = 243,
		HeBei = 250,
		XinJiang = 346,
		LiaoNing = 349,
		HuNan = 350,
		JiLin = 351,
		GuangXi = 352,
		SiChuan = 353,
		HaiNan = 354,
		ZheJiang = 355,
		QingHai = 356,
		JiangSu = 357
	}

    public enum IspEnum
	{
		Telecom = 1,
		Unicom = 2,
		Mobile = 7
	}

    public enum StatusEnum
	{
		disabled,
		enabled
	}

    public enum BrowserType
	{
		Chrome = 1,
		Firefox = 2
	}

    public enum TestType
	{
		Performance = 1,
		Monitoring = 2
	}

    public enum AlertType
    {
        Server = 0,
        Domain = 1
    }
}