using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lang : MonoBehaviour {

	public static Dictionary<string,string> mTurkish = new Dictionary<string,string>();
	public static Dictionary<string,string> mEnglish = new Dictionary<string,string>();

	void Awake(){

		//strings in English
		mTurkish.Add ("play", "OYNA");
		mTurkish.Add ("newgame", "YENİ OYUN");
		mTurkish.Add ("settings", "AYARLAR");
		mTurkish.Add ("load", "YÜKLE");
		mTurkish.Add ("quit", "ÇIKIŞ");
		mTurkish.Add ("houserules", "EV KURALLARI");
		mTurkish.Add ("accepthouserules", "EV KURALLARINI KABUL ET");
		mTurkish.Add ("resethouserules", "EV KURALLARINI SIFIRLA");
		mTurkish.Add ("back", "GERI");
		mTurkish.Add ("addplayer", "OYUNCU EKLE");
		mTurkish.Add ("remove", "KALDIR");
		mTurkish.Add ("delete", "SİL");
		mTurkish.Add ("cratecountry", "ÜLKE YARAT");
		mTurkish.Add ("update", "GÜNCELLE");
		mTurkish.Add ("selectcountry", "ÜLKE SEÇ");
		mTurkish.Add ("auction", "AÇIK ARTIRMA");
		mTurkish.Add ("build", "İNŞA ET");
		mTurkish.Add ("buy", "SATIN AL");
		mTurkish.Add ("finishturn", "SIRAYI BİTİR");	
		mTurkish.Add ("mortgage", "İPOTEK");
		mTurkish.Add ("offer", "TEKLİF ET");
		mTurkish.Add ("decline", "REDDET");
		mTurkish.Add ("options", "SEÇENEKLER");
		mTurkish.Add ("summary", "ÖZET");
		mTurkish.Add ("rolldice", "ZAR AT");
		mTurkish.Add ("sell", "SATIŞ YAP");
		mTurkish.Add ("save", "KAYDET");
		mTurkish.Add ("trade", "TAKAS");
		mTurkish.Add ("unmortgage", "İPOTEK KALDIR");
		mTurkish.Add ("done", "BİTTİ");	
		mTurkish.Add ("ok", "TAMAM");
		mTurkish.Add ("close", "KAPAT");
		mTurkish.Add ("previous", "ÖNCEKİ");
		mTurkish.Add ("next", "SONRAKİ");
		mTurkish.Add ("cancel", "İPTAL");
		mTurkish.Add ("resume", "DEVAM ET");
		mTurkish.Add ("bid", "TEKLİF ET");
		mTurkish.Add ("fold", "ÇEKİL");
	

		//strins in English
		mEnglish.Add ("play", "PLAY");
		mEnglish.Add ("newgame", "NEW GAME");
		mEnglish.Add ("settings", "SETTINGS");
		mEnglish.Add ("load", "LOAD");
		mEnglish.Add ("quit", "QUIT");
		mEnglish.Add ("houserules", "HOUSE RULES");
		mEnglish.Add ("accepthouserules", "ACCEPT HOUSE RULES");
		mEnglish.Add ("resethouserules", "RESET HOUSE RULES");
		mEnglish.Add ("back", "BACK");
		mEnglish.Add ("addplayer", "ADD PLAYER");
		mEnglish.Add ("remove", "REMOVE");
		mEnglish.Add ("delete", "DELETE");
		mEnglish.Add ("cratecountry", "CREATE COUNTRY");
		mEnglish.Add ("update", "UPDATE");
		mEnglish.Add ("selectcountry", "SELECT COUNTRY");
		mEnglish.Add ("auction", "AUCTION");
		mEnglish.Add ("build", "BUILD");
		mEnglish.Add ("buy", "BUY");
		mEnglish.Add ("finishturn", "FINISH TURN");	
		mEnglish.Add ("mortgage", "MORTGAGE");
		mEnglish.Add ("offer", "OFFER");
		mEnglish.Add ("decline", "DECLINE");
		mEnglish.Add ("options", "OPTIONS");
		mEnglish.Add ("summary", "SUMMARY");
		mEnglish.Add ("rolldice", "ROLL DICE");
		mEnglish.Add ("sell", "SELL");
		mEnglish.Add ("save", "SAVE");
		mEnglish.Add ("trade", "TRADE");
		mEnglish.Add ("unmortgage", "UNMORTGAGE");
		mEnglish.Add ("done", "DONE");	
		mEnglish.Add ("ok", "OK");
		mEnglish.Add ("close", "CLOSE");
		mEnglish.Add ("previous", "PREVIOUS");
		mEnglish.Add ("next", "NEXT");
		mEnglish.Add ("cancel", "CANCEL");
		mEnglish.Add ("resume", "RESUME");
		mEnglish.Add ("bid", "BID");
		mEnglish.Add ("fold", "FOLD");

	
	
	}

	public static string giveMeText(string keyS){
		string res="";
		string langChosen="";
		langChosen=PlayerPrefs.GetString ("language");
		if (langChosen == "" || langChosen == "en") {
			//print ("lang is eng");
			res=mEnglish[keyS];
		}
		else if (langChosen == "tr"){
			//print ("lang is Turkish");	
			res=mTurkish[keyS];
		}
		return res;

	}

}
