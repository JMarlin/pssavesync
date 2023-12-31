
#		-Uri "http://localhost:7071/api/PushSave" `

function getSaveDetails([string] $cardPath) {
	Invoke-WebRequest `
		-Method POST `
		-Uri "https://psavesync-functions.azurewebsites.net/api/PushSave" `
		-Body "$([Convert]::ToBase64String([System.IO.File]::ReadAllBytes($cardPath)))" `
		-ContentType "application/json" `
		-SkipCertificateCheck
}

function dumpAllSaveData() {
	ls /Volumes/Untitled/PSP/SAVEDATA/SLUS0*/SCEVMC*.VMP `
	| % { getSaveDetails $_ }
}
