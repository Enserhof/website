<template>
  <div class="container mx-auto px-4 flex flex-col gap-6">
    <h1 class="text-3xl font-bold">Angebote</h1>
    <div class="flex flex-col gap-6">
      <div class="flex flex-col gap-4 bg-white p-8 shadow-md/50 rounded-xl">
        <h2 class="text-2xl font-semibold">Ponyclub</h2>
        <div class="content flex flex-col gap-4">
          <ul>
            <li>Jeden Samstag in den Sommerferien von 09:00 Uhr bis 10:00 Uhr</li>
            <li>Gemeinsames Putzen, Striegeln und Hufe auskratzen</li>
            <li>Jedes Kind wird beliebig oft jeweils zwei Runden in der Koppel geführt</li>
            <li>Zwei "Warteschlangen" für Leika und Luna</li>
            <li>Erwachsene können im Stüberl die kleinen Reiter bei Kaffee oder Saft beobachten</li>
          </ul>
          <p><b>Kosten:</b> 5 € pro Kind</p>
          <p class="flex flex-wrap gap-4 align-end">
            <img src="@/assets/images/angebote/ponyclub_1.jpg">
            <img src="@/assets/images/angebote/ponyclub_2.jpg">
            <img src="@/assets/images/angebote/ponyclub_3.jpg">
            <img src="@/assets/images/angebote/ponyclub_4.jpg">
            <img src="@/assets/images/angebote/ponyclub_5.jpg">
            <img src="@/assets/images/angebote/ponyclub_6.jpg">
          </p>
        </div>
      </div>
      <div class="flex flex-col gap-4 bg-white p-8 shadow-md/50 rounded-xl">
        <h2 class="text-2xl font-semibold">Ponyreiten</h2>
        <div class="content flex flex-col gap-4">
          <p>Die Möglichkeiten zum Ponyreiten richten sich hauptsächlich nach deinem Alter, deiner Erfahrung mit Ponys und Tieren im Allgemeinen und nach deinem Geschick.</p>
          <h3 class="text-xl font-semibold">Minireiter (2 - 3 Jahre, ca. 30 Minuten) - der erste Kontakt mit Ponys</h3>
          <p>Gemeinsam wird unser Pony Leika geputzt, um eine Verbindung zwischen dir und dem Pferd aufzubauen.
            Anschließend reinigen wir gemeinsam die Hufe und satteln das Pony. Jetzt geht es los.
            Während du dich gut am Haltegurt festhältst, reiten wir ein paar Runden in unserer Koppel - du bist aber natürlich nicht alleine, sondern wirst geführt.
            Am Ende verabschieden wir uns wieder von Leika und belohnen sie noch mit einer Kleinigkeit.</p>
          <p><b>Kosten:</b> 12 €</p>
          <h3 class="text-xl font-semibold">Kinderreiter (4 - 7 Jahre, ca. 45 Minuten)</h3>
          <p>Zu Beginn darfst du eines unserer Ponys putzen, um eine Verbindung mit ihm aufzubauen und Selbstbewusstsein im Umgang mit Pferden zu erlangen.
            Anschließend werden die Hufe kontrolliert und gereinigt und dann das Pony gesattelt.
            Je nach Erfahrung und Geschick verwenden wir entweder den Haltegurt oder einen englischen Sattel, wo du dich gut halten kannst.
            Beim Kinderreiten bleiben wir auf unserer Hofwiese und machen Übungen am Pony.</p>
          <p><b>Kosten:</b> 18 €</p>
          <h3 class="text-xl font-semibold">Ausritt (ab 6 Jahren, ca. 1 - 1 ½ Stunden)</h3>
          <p>Nach dem gemeinsamen Putzen, Hufe reinigen und satteln machen wir uns auf den Weg zu unserem nahegelegenen Wald.
            Auf dich wartet viel Natur, eine wundervolle Kulisse und ein kleines Abenteuer durch den Wald.</p>
          <p><b>Kosten:</b> 24 - 36 €</p>
        </div>
      </div>
      <div class="flex flex-col gap-4 bg-white p-8 shadow-md/50 rounded-xl">
        <h2 class="text-2xl font-semibold">Esel- oder Ziegenwanderung</h2>
        <div class="content flex flex-col gap-4">
          <p>Ihr möchtet wandern, entschleunigen und dabei in tierischer Begleitung sein?
            Dann entführt doch unsere Esel Paula und Lilly oder unsere Ziegen Lisi und Sissy zu einem Spaziergang.</p>
          <p>Der ruhige und langsame Gang der Esel wirkt besonders entspannend und macht eure Wanderung zu einer Wohltat für unruhige Geister.
            Die Ziegen sind dafür umso lebhafter und sorgen mit ihren Kletterkünsten und ihrer neugierigen Art immer für ein Schmunzeln.</p>
          <p>Bevor es losgeht, werden die Esel geputzt und ihre Hufe gereinigt. Die Ziegen sind sofort startklar.
            Dann versorgen wir euch noch mit ein paar nützlichen Hinweisen für den Umgang mit euren tierischen Begleitern und wünschen euch eine nette Zeit.</p>
          <p>Für die Wanderung sollten mindestens zwei Erwachsene dabei sein, denn sowohl die Esel als auch die Ziegen
            sind nicht gerne alleine und können auch mal streiken, wenn sie sich zu weit voneinander entfernen.</p>
          <div>
            <b>Kosten:</b>
            <ul>
              <li>Spritztour (bis 2 Stunden): 20 €</li>
              <li>Halbtageswanderung (bis 4 Stunden): 30 €</li>
              <li>Ganztageswanderung (nur mit den Eseln, bis 8 Stunden): 50 €</li>
            </ul>
          </div>
        </div>
      </div>
      <div class="flex flex-col gap-4 bg-white p-8 shadow-md/50 rounded-xl">
        <h2 class="text-2xl font-semibold">Eier von Freilandhühnern</h2>
        <div class="content flex flex-col gap-4">
          <p>Unsere kleine Hühnerschar wird mit Getreide gefüttert und biologisch gehalten. Die Eier sind direkt bei uns erhältlich.</p>
          <div>
            <b>Kosten:</b>
            <ul>
              <li>Sechserpackung: 3 €</li>
              <li>Zehnerpackung: 5 €</li>
            </ul>
          </div>
        </div>
      </div>
      <div class="flex flex-col gap-4 bg-white p-8 shadow-md/50 rounded-xl">
        <h2 class="text-2xl font-semibold">Ziegenmilch</h2>
        <div class="content flex flex-col gap-4">
          <p>Die Ziegen werden zweimal pro Tag gemolken, anschließend wird die Milch gereinigt und ist als Rohmilch bei uns erhältlich.</p>
          <div>
            <b>Kosten:</b>
            <ul>
              <li>½ Liter: 1 €</li>
              <li>1 Liter: 2 €</li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
