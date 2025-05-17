<script setup lang="ts">
import AnimalBox from './AnimalBox.vue'
import eselImage from '@/assets/images/tiere/esel.jpg'
import ponysImage from '@/assets/images/tiere/ponys.jpg'
import huehnerImage from '@/assets/images/tiere/huehner.jpg'
import ziegenImage from '@/assets/images/tiere/ziegen.jpg'
import hundImage from '@/assets/images/tiere/hund.jpg'
import hasenImage from '@/assets/images/tiere/hasen.jpg'
import katzenImage from '@/assets/images/tiere/lilo.jpg'
</script>

<template>
  <div class="container mx-auto px-4 flex flex-col gap-6">
    <h1 class="text-3xl font-bold">Über den Hof</h1>
    <div class="flex flex-col gap-2">
      <p>Der Enserhof ist seit vier Generationen ein Familienbetrieb.</p>
      <p>Früher wurde er vor allem zu Selbstversorgungszwecken genützt.</p>
      <p>Bis 1995 wurden hauptsächlich Kühe zur Milchproduktion gehalten.</p>
      <p>Zwischen 1995 und 2014 war der Betrieb dann eine reine Schweinezucht und -mast.</p>
      <p>Wir (Sylvia und Johannes) sind seit 2011 auf dem Hof.</p>
      <p>2012 sind die beiden Esel Lilly und Paula als Ausgleich vom Arbeitsalltag zu uns gekommen.</p>
      <p>2016 ist der Vierkanthof durch die Entfernung von zwei Seiten geöffnet worden und bietet seit dem Platz für unseren Garten.</p>
    </div>

    <h2 class="text-2xl font-semibold">Tiere</h2>
    <div class="flex flex-col md:flex-row gap-6">
      <div class="basis-2/3 flex flex-col gap-6">
        <AnimalBox title="Lilly & Paula" :image="eselImage">
          <p>Unsere zwei Esel waren unsere ersten Begleiter. Sie kamen im Alter von ein (Lilly) und zwei (Paula) Jahren zu uns.</p>
          <p>Die beiden lieben sich gegenseitig heiß und es wäre unmöglich, sie zu trennen.</p>
          <p>Wenn wir mit ihnen fort gehen, dann zu zweit. Paula ist die Leitstute und eine starke Fresserin - sie vertreibt alle Tiere unserer Herde.</p>
          <p>Lilly ist diesbezüglich etwas entspannter. Sie ist aber beim Spazierengehen vorne auf.</p>
          <p>Unsere Esel bekommen von uns Heu, Stroh, einen Leckstein und etwas Gras zum Fressen.</p>
          <p>Brot und Obst haben zu viel Kohlenhydrate &ndash; das vertragen unsere Esel nicht!</p>
          <p>Deshalb die beiden bitte nicht mehr füttern. Danke!</p>
        </AnimalBox>
        <AnimalBox title="Leika & Luna" :image="ponysImage">
          <p>Unser Pony Leika ist 2017 zu uns auf den Hof gekommen. Sie ist mittlerweile 10 Jahre alt und eine sehr angenehme und ruhige Stute.</p>
          <p>Im April 2018 überraschte sie uns mit ihrem Fohlen "Luna".</p>
          <p>Bezüglich dem Fressen gilt das gleiche wie bei den Eseln.</p>
        </AnimalBox>
        <AnimalBox title="Hühner" :image="huehnerImage">
          <p>Unsere kleine Hühnerschar besteht aktuell aus fünf Hühnern. Sie lieben es sich unter den Sträuchern zu verkriechen und sich in der Erde zu wälzen und sind sogar den Umgang mit Kleinkindern gewohnt.</p>
        </AnimalBox>
      </div>
      <div class="basis-1/3 flex flex-col gap-6">
        <AnimalBox title="Lisa & Sissy" :image="ziegenImage">
          <p>Lisa und Sissy sind 2020 als kleine Zicklein von einer befreundeten Ziegenlandwirtschaft zu uns gekommen. Seit 2022 bekamen wir zunächst von beiden, später nur noch von Lisa, ca. zwei Liter Milch pro Tag.</p>
        </AnimalBox>
        <AnimalBox title="Puma" :image="hundImage">
          <p>Unser Wachhund Puma lebt seit 2015 auf unserem Hof. Puma ist ein sehr netter Kerl. Er liebt Kinder über alles und tut keiner Mücke etwas zu Leide. Puma liebt es, im Garten (oder in der Sandkiste) zu spielen.</p>
        </AnimalBox>
        <AnimalBox title="Tina" :image="hasenImage">
          <p>Unsere Häsin ist aktuell eine Einzelgängerin - sie wird aber in naher Zukunft wieder Gesellschaft bekommen. Die Hasen werden gerne von den Kindern gefüttert, gestreichelt und beobachtet.</p>
        </AnimalBox>
        <AnimalBox title="Lilo, Susi & Billy" :image="katzenImage">
          <p>Lilo ist uns im Herbst 2018 in einer stürmigen Nacht zugelaufen. Wir versuchten zwar, den Besitzer ausfindig zu machen, insgeheim hofften wir aber von Anfang an, dass Lilo bei uns bleiben kann. Sie ist eine äußerst zutrauliche Katze, will immer voll dabei sein und folgt oft auf Schritt und Tritt. Bei der Mäusejagd ist sie unschlagbar - vor allem, wenn sie ihre Jungen versorgen muss.</p>
          <p>Lilo erfreut uns regelmäßig (ca. zwei Mal pro Jahr) mit Nachwuchs. Beim ersten Wurf war Susi eines dieser Geschenke.</p>
          <p>Billy hingegen ist das Überbleibsel eines Nachwuchses einer anderen Katzenmutter, die ihre beiden Jungen in unserem Heuboden zur Welt gebracht hat, einige Wochen später aber dann nur mit dem Geschwister weiterzog. </p>
        </AnimalBox>
      </div>
    </div>
  </div>
</template>
