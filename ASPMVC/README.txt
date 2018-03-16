Szkic aplikacji asp.net - notatki

Global.asax - plik wejsciowy serwera. Definiowanie tablicy routingu

RouteConfig.cs - definiowanie zapisu URL ../{x}/{y}-{z}


Wdrażanie aplikacji:
	Hostowanie na komputerze(Win):
		- Windows Server (może być zwykły Windows)
		- Zainstalowana usługa IIS
		- Baza danych np. SQL Express
	Hostowanie na komputerze(Inne):
		- Linux
		- Mono
		- Apache
	Usługa hostingowa:
		- Azure
			

	Paczka do wdrażania:
		- kompilacja *.cs do *.dll (pliki CSHTML nie)
		- usunięcie debug="true" z web.config
		- podmiana connection string do bazy danych
		- wyłącznie custom error (zamiana na domyślną strone error.html)

	Możemy ustawić własne ustawienia konfiguracji. Web.config ma również pliki Web.Debug.config oraz Web.Release.config które
	mogą posłużyć jako transformatory xdt. W tych miejscach możemy transformatować connection stringi, usuwać niektóre linie kodu
	z Web.config w zależności od konfiguracji (Debug/Release lub własny). Sprawdź 'Web.Release - IIS.config' gdzie zamiana następuje na 
	DbCourseEntities (Preview Transform). 